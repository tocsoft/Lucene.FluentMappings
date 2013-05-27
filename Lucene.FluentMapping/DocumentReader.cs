using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.FluentMapping.Configuration;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping
{
    public class DocumentReader<T>
    {
        private readonly Func<T> _create;
        private readonly IList<IFieldReader<T>> _readers;

        public DocumentReader(Func<T> create, IEnumerable<IFieldReaderFactory<T>> mappings)
        {
            _create = create;
            _readers = mappings.Select(x => x.CreateFieldReader()).ToList();
        }

        public T Read(Document source)
        {
            var instance = _create();

            foreach (var reader in _readers)
                reader.Apply(source, instance);

            return instance;
        }
    }
}