using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public interface IFieldWriterFactory<T>
    {
        IFieldWriter<T> CreateFieldWriter();
    }
}