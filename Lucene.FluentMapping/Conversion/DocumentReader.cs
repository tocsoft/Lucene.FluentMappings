using System;
using System.Collections.Generic;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class DocumentReader<T>
    {
        private readonly Func<T> _create;
        private readonly IEnumerable<IFieldReader<T>> _mappings;

        public DocumentReader(Func<T> create, IEnumerable<IFieldReader<T>> mappings)
        {
            _create = create;
            _mappings = mappings;
        }

        public T Read(Document source)
        {
            var instance = _create();

            foreach (var mapping in _mappings)
                mapping.ValueFrom(source).Apply(instance);

            return instance;
        }
    }
}