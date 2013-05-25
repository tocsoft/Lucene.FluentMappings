using System;
using Lucene.Net.Documents;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    [TestFixture]
    public class DocumentMapperFixture
    {
        private DocumentMapper<Advert> _mapper;
        private Advert _advert;

        [SetUp]
        public void SetUp()
        {
            _mapper = DocumentMapper.For<Advert>();

            _advert = new Advert
                {
                    CategoryId = 123,
                    CategoryName = "bookcases",
                    Colour = "natural oak",
                    Depth = 200,
                    Height = 2000,
                    Width = 600,
                    Description = "a very nice bookcase",
                    Title = "oak bookcase",
                    Id = 999,
                    Price = 400,
                    PrimaryImageUri = new Uri("http://www.example.com/foo"),
                    Uri = new Uri("http://www.example.com/bar")
                };
        }

        [Test]
        public void CanCovertToDocumentFromDefaultInstance()
        {
            var document = _mapper.Convert(new Advert());

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