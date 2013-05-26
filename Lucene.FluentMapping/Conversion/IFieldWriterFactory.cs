namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldWriterFactory<T>
    {
        IFieldWriter<T> CreateFieldWriter();
    }
}