namespace CatMash.Common.Config
{
    public interface IConfigRepo<TConfig>
    {
        TConfig Configuration { get; }
    }
}
