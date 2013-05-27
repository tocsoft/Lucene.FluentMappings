using System;

namespace Lucene.FluentMapping.Conversion
{
    public interface IConfigurableFieldMap<TOptions>
    {
        void Configure(Action<TOptions> configure);
    }
}