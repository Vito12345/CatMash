using System;

namespace CatMash.Common.Config
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConfigurationPropertyAttribute : System.Attribute
    {
        public ConfigurationPropertyAttribute(bool IsRequired = false, object DefaultValue = null)
        {

        }
        public bool IsRequired { get; set; }
        public object DefaultValue { get; set; }
    }
}
