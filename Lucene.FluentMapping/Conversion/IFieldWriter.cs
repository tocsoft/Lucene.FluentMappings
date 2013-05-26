using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldWriter<T>
    {
        // TODO re-use IFieldable impls - they are expensive to create!

        IFieldable GetField(T instance);
    }
}