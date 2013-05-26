using Lucene.FluentMapping.Configuration;
using Lucene.FluentMapping.Conversion;
using Lucene.FluentMappings.Demo;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    [TestFixture]
    public class DocumentWriterFixture
    {
        private DocumentWriter<Advert> _writer;

        [SetUp]
        public void SetUp()
        {
            var mappings = MappingFactory.GetMappings<Advert>();
            _writer = new DocumentWriter<Advert>(mappings);
        }

        [Test]
        public void CanCovertToDocumentFromDefaultInstance()
        {
            var document = _writer.Write(new Advert(0, null));

            Assert.That(document, Is.Not.Null);
        }

        [Test]
        public void WhenConvertingFromInstance_DocumentHasFieldsForEachProperty()
        {
            var document = _writer.Write(Example.Advert());

            Assert.That(document, Is.Not.Null
                                    .And.Property("fields_ForNUnit").Count.EqualTo(12));
        }
    }
}