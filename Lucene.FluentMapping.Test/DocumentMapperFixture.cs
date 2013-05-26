using Lucene.FluentMappings.Demo;
using Lucene.Net.Documents;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    [TestFixture]
    public class DocumentMapperFixture
    {
        private IDocumentMapper<Advert> _mapper;
        private Advert _advert;

        [SetUp]
        public void SetUp()
        {
            _mapper = DocumentMapper.For(() => new Advert(0, string.Empty));

            _advert = Example.Advert();
        }

        [Test]
        public void CanCovertToDocumentFromDefaultInstance()
        {
            var document = _mapper.Convert(new Advert(0, null));

            Assert.That(document, Is.Not.Null);
        }

        [Test]
        public void CanCovertFromEmptyDocumentToInstance()
        {
            var instance = _mapper.Convert(new Document());

            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void WhenConvertingFromInstance_DocumentHasFieldsForEachProperty()
        {
            var document = _mapper.Convert(_advert);

            Assert.That(document, Is.Not.Null
                                    .And.Property("fields_ForNUnit").Count.EqualTo(12));
        }
        
        [Test]
        public void CanRoundTripConvert()
        {
            var document = _mapper.Convert(_advert);

            var roundTripped = _mapper.Convert(document);
            
            var differences = Compare.Properties(_advert, roundTripped);

            Assert.That(differences, Is.Empty);
        }
    }
}