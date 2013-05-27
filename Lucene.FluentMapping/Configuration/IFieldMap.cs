namespace Lucene.FluentMapping.Configuration
{
    public interface IFieldMap<T> : IFieldWriterFactory<T>, IFieldReaderFactory<T>
    { }
}