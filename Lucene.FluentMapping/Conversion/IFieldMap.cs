namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldMap<T> : IFieldWriter<T>, IFieldReader<T>
    { }
}