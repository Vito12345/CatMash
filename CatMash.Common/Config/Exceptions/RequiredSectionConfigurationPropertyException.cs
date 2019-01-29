using System;

namespace CatMash.Common.Config.Exceptions
{
    internal class RequiredSectionConfigurationPropertyException : Exception
    {
        public RequiredSectionConfigurationPropertyException(string propertyName) : base(propertyName) { }

    }
}
