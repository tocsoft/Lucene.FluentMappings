using System;

namespace Lucene.FluentMapping.Conversion
{
    public interface IConfigurable<TFieldMap, TOptions>
    {
        IFieldMap<TFieldMap> Configure(Action<TOptions> configure);
    }
}