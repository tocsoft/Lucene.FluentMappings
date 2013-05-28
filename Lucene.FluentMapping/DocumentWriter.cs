using System.Collections.Generic;
using System.Linq;
using Lucene.FluentMapping.Configuration;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping
{
    /// <summary>
    /// Thread-safe
    /// </summary>
    public class DocumentWriter<T>
    {
        private readonly List<IFieldWriter<T>> _writers;

        private readonly Document _document;

        public DocumentWriter(IEnumerable<IFieldWriterFactory<T>> mappings, Document document = null)
        {
            _writers = mappings.Select(x => x.CreateFieldWriter()).ToList();
            
            _document = document ?? new Document();
            
            foreach (var writer in _writers)
                _document.Add(writer.Field);
        }

        public Document UpdateFrom(T source)
        {
            foreach (var writer in _writers)
                writer.WriteValueFrom(source);

            return _document;
        }
    }
}