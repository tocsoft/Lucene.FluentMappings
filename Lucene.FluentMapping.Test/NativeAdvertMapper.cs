using Lucene.FluentMapping.Conversion;
using Lucene.FluentMappings.Demo;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Test
{
    public class NativeAdvertMapper : IDocumentMapper<Advert>
    {
        public Document Convert(Advert source)
        {
            var document = new Document();

            // TODO map fields

            return document;
        }

        public Advert Convert(Document doc)
        {
            throw new System.NotImplementedException();
        }
    }
}