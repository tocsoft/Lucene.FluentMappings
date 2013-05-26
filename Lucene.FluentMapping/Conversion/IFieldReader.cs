using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldReader<T>
    {
        Setter<T> ValueFrom(Document document);
    }
}