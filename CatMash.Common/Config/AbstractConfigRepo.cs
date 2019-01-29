namespace CatMash.Common.Config
{
    public abstract class AbstractConfigRepo<TConfig, TConfigSection> : IConfigRepo<TConfig>
        where TConfigSection : class, IConfigSection, new()
    {
        public AbstractConfigRepo(IConfigSectionValidator<TConfigSection> configSectionValidator)
        {
            configSectionValidator.ValidateSection();

            Configuration = ToConfig(configSectionValidator.ConfigSection);
        }

        public TConfig Configuration { get; }

        public abstract TConfig ToConfig(TConfigSection section);
    }
}
