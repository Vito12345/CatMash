using CatMash.Common.Config;
using CatMash.Domain.Configuration;

namespace CatMash.Infra.Data.Configuration
{
    public class CatMashConfigRepo : AbstractConfigRepo<CatMashConfig, CatMashConfigSection>
    {
        public CatMashConfigRepo(IConfigSectionValidator<CatMashConfigSection> configSectionValidator) : base(configSectionValidator)
        {
        }

        public override CatMashConfig ToConfig(CatMashConfigSection section)
        {
            if (section == null)
                return null;

            return new CatMashConfig
            {
                SourceJsonFile = section.SourceJsonFile
            };
        }
    }
}
