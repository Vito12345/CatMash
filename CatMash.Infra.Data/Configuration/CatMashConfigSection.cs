using CatMash.Common.Config;

namespace CatMash.Infra.Data.Configuration
{
    public class CatMashConfigSection : IConfigSection
    {
        /// <summary>
        /// JSON file path with sources
        /// </summary>
        [ConfigurationProperty(IsRequired = true)]
        public string SourceJsonFile { get; set; }
    }
}
