using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CatMash.Common.Config.Exceptions;
using Microsoft.Extensions.Options;

namespace CatMash.Common.Config
{
    public class ConfigSectionValidator<TConfigSection> : IConfigSectionValidator<TConfigSection>
        where TConfigSection : class, IConfigSection, new()
    {
        public TConfigSection ConfigSection { get; private set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public ConfigSectionValidator(IOptions<TConfigSection> section)
        {
            ConfigSection = section.Value;
        }

        public void ValidateSection()
        {
            if (!ValidateSection(ConfigSection))
            {
                throw new ConfigurationSectionValidationException(typeof(TConfigSection).Name);
            }
        }

        private bool ValidateSectionItems<T>(T sectionItems) where T : IEnumerable<IConfigSection>
        {
            var isValid = true;
            foreach (var section in sectionItems)
            {
                if (!ValidateSection(section) && isValid)
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool ValidateSection<T>(T section) where T : IConfigSection
        {
            var isValid = true;
            var properties = section.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (ValidateProperty(section, prop))
                {
                    if (IsIConfigSectionCollectionType(prop.PropertyType))
                    {
                        if (!ValidateSectionItems(prop.GetValue(section) as IEnumerable<IConfigSection>) && isValid)
                        {
                            isValid = false;
                        }
                    }
                    else if (IsIConfigSectionType(prop.PropertyType))
                    {
                        if (!ValidateSection(prop.GetValue(section) as IConfigSection) && isValid)
                        {
                            isValid = false;
                        }
                    }
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool IsIConfigSectionCollectionType(Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(IList<>)
                && IsIConfigSectionType(type.GetGenericArguments().First());
        }

        private bool IsIConfigSectionType(Type type)
        {
            return typeof(IConfigSection).IsAssignableFrom(type);
        }

        private bool ValidateProperty<T>(T obj, PropertyInfo prop) where T : IConfigSection
        {
            var isValid = true;

            if (Attribute.IsDefined(prop, typeof(ConfigurationPropertyAttribute)))
            {
                try
                {
                    var attribute = (ConfigurationPropertyAttribute)Attribute.GetCustomAttribute(prop, typeof(ConfigurationPropertyAttribute));
                    var value = prop.GetValue(obj);

                    if (value == null && attribute.IsRequired)
                    {
                        throw new RequiredSectionConfigurationPropertyException(prop.Name);
                    }

                    if (value == null && attribute.DefaultValue != null)
                    {
                        prop.SetValue(obj, attribute.DefaultValue);
                    }

                    if (value == null && attribute.DefaultValue == null)
                    {
                        if (prop.PropertyType.IsValueType && prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            value = Activator.CreateInstance(prop.PropertyType.GetGenericArguments().First());
                            prop.SetValue(obj, value);
                        }
                    }
                }
                catch (RequiredSectionConfigurationPropertyException ex)
                {
                    //_logger.Error($"Property {prop.Name} is declared as Required in Section {typeof(T).Name}", ex);
                    isValid = false;
                }
                catch (Exception ex)
                {
                    //_logger.Error("Error on configuration", ex);
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
