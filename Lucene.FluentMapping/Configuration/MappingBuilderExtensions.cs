using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class MappingBuilderExtensions
    {
        public static MappingBuilder<T> Timestamp<T>(this MappingBuilder<T> @this, string name)
        {
            return @this.Add(new TimestampMapping<T>(name));
        }

    }
}