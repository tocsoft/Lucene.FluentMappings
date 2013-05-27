using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldReader<T>
    {
        void Apply(Document document, T instance);
    }
}