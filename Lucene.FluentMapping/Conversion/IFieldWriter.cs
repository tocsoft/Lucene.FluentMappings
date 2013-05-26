namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldWriter<T>
    {
        IField<T> CreateField();
    }
}