using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldWriter<T>
    {
        IFieldable GetField(T instance);
    }
}