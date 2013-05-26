using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldWriter<T>
    {
        IFieldable Field { get; }

        void WriteValueFrom(T instance);
    }
}