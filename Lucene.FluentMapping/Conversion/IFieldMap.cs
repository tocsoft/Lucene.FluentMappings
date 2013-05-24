using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldMap<T>
    {
        IFieldable GetField(T instance);

        Setter<T> ValueFrom(Document document);
    }
}