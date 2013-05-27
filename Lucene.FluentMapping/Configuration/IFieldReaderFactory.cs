using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public interface IFieldReaderFactory<T>
    {
        IFieldReader<T> CreateFieldReader();
    }
}