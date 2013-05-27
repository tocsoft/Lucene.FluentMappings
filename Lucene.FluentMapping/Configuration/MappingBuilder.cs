using System.Collections.Generic;

namespace Lucene.FluentMapping.Configuration
{
    public class MappingBuilder<T>
    {
        private readonly List<IFieldMap<T>> _mappings = new List<IFieldMap<T>>();

        public IEnumerable<IFieldMap<T>> Mappings
        {
            get { return _mappings; }
        }

        public TMap Map<TMap>(TMap fieldMapping)
            where TMap : IFieldMap<T>
        {
            _mappings.Add(fieldMapping);

            return fieldMapping;
        }
    }
}