namespace Lucene.FluentMapping.Configuration
{
    public static class MappingBuilderExtensions
    {
        public static IFieldMap<T> Timestamp<T>(this MappingBuilder<T> @this, string name)
        {
            return @this.Map(new TimestampMapping<T>(name));
        }
    }
}