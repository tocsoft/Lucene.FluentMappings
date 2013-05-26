using System.Collections.Generic;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class DocumentWriter<T>
    {
        private readonly IEnumerable<IFieldMap<T>> _mappings;

        public DocumentWriter(IEnumerable<IFieldMap<T>> mappings)
        {
            _mappings = mappings;
        }

        public Document Write(T source)
        {
            var document = new Document();

            foreach (var mapping in _mappings)
            {
                var field = mapping.GetField(source);

                if (field != null)
                    document.Add(field);
            }

            return document;
        }
    }
}