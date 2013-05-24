namespace Lucene.FluentMapping.Configuration
{
    public static class Mappings
    {
        public static MappingBuilder<T> For<T>()
        {
            return new MappingBuilder<T>();
        }
    }
}