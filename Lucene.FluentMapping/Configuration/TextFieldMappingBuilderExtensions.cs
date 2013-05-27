using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class TextFieldMappingBuilderExtensions
    {
        public static IConfigurableFieldMap<TextFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, string>> property)
        {
            return @this.Map(FieldMap.For(property, new StringFieldAccessor()));
        }
        
        public static IConfigurableFieldMap<TextFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, Uri>> property)
        {
            return @this.Map(FieldMap.For(property, new UriFieldAccessor()));
        }
    }
}