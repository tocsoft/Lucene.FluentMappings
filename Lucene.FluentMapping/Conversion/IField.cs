using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public interface IField<in T>
    {
        void SetValueFrom(T instance);

        IFieldable Field { get; }
    }
}