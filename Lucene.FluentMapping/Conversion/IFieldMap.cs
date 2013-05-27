namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldMap<T> : IFieldWriterFactory<T>, IFieldReaderFactory<T>
    { }
}