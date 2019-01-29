namespace CatMash.Common.Config
{
    public interface IConfigSectionValidator<TConfigSection> where TConfigSection : IConfigSection
    {
        TConfigSection ConfigSection { get; }

        void ValidateSection();
    }
}
