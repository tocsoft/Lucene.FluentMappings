using System;

namespace Lucene.FluentMapping.Configuration
{
    public static class ConfigurableFieldMapExtensions
    {
        public static void Configure<TOptions>(this IConfigurableFieldMap<TOptions> @this, Action<TOptions> configure)
        {
            if (configure != null)
                configure(@this.Options);
        }
    }
}