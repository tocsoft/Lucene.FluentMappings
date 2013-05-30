using Lucene.FluentMapping.Configuration;
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
            var mappings = MappingFactory<Advert>.GetMappings();
            _writer = new DocumentWriter<Advert>(mappings);
        }

        [Test]
        public void CanCovertToDocumentFromDefaultInstance()
        {
            _writer.UpdateFrom(new Advert(0, null));

            Assert.That(_writer.Document, Is.Not.Null);
        }

        [Test]
        public void WhenConvertingFromInstance_DocumentHasFieldsForEachProperty()
        {
            _writer.UpdateFrom(Example.Advert());

            var propertyCount = typeof (Advert).GetProperties().Length;

            Assert.That(_writer.Document, Is.Not.Null
                                    .And.Property("fields_ForNUnit").Count.EqualTo(propertyCount));
        }
    }
}