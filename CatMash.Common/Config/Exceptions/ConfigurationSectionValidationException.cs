using System;

namespace CatMash.Common.Config.Exceptions
{
    public class ConfigurationSectionValidationException : Exception
    {
        public ConfigurationSectionValidationException(string sectionName) : base(sectionName) { }
    }
}
