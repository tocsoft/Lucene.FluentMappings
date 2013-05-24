using System.Collections.Generic;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public class MappingBuilder<T>
    {
        private readonly List<IFieldMap<T>> _mappings = new List<IFieldMap<T>>();

        private IEnumerable<IFieldMap<T>> Mappings
        {
            get { return _mappings; }
        }

        public MappingBuilder<T> Add(IFieldMap<T> fieldMapping)
        {
            _mappings.Add(fieldMapping);

            return this;
        }

        // HACK i'm not sure this is a good idea, but it's interesting!
        // NOTE implicit conversions won't return interface types
        public static implicit operator List<IFieldMap<T>>(MappingBuilder<T> builder)
        {
            return builder._mappings;
        }
    }
}