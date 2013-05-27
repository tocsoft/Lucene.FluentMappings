namespace Lucene.FluentMapping.Configuration
{
    public interface IConfigurableFieldMap<TOptions>
    {
        TOptions Options { get; }
    }
}