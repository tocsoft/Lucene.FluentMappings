using Lucene.FluentMapping.Configuration;
using Lucene.FluentMappings.Demo;
using Lucene.Net.Documents;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    [TestFixture]
    public class DocumentReaderFixture
    {
        private DocumentReader<Advert> _reader;

        [SetUp]
        public void SetUp()
        {
            var mappings = MappingFactory.GetMappings<Advert>();
            _reader = new DocumentReader<Advert>(() => new Advert(), mappings);
        }

        [Test]
        public void CanCovertFromEmptyDocumentToInstance()
        {
            var instance = _reader.Read(new Document());

            Assert.That(instance, Is.Not.Null);
        }
    }
}