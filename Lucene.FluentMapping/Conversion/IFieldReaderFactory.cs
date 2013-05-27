namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldReaderFactory<T>
    {
        IFieldReader<T> CreateFieldReader();
    }
}