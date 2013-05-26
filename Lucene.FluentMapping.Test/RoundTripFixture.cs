using System.Linq;
using Lucene.FluentMapping.Configuration;
using Lucene.FluentMapping.Conversion;
using Lucene.FluentMappings.Demo;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    [TestFixture]
    public class RoundTripFixture
    {
        private DocumentReader<Advert> _reader;
        private DocumentWriter<Advert> _writer;
        private Advert _advert;

        [SetUp]
        public void SetUp()
        {
            var mappings = MappingFactory.GetMappings<Advert>().ToList();
            _reader = new DocumentReader<Advert>(() => new Advert(), mappings);
            _writer = new DocumentWriter<Advert>(mappings);

            _advert = Example.Advert();
        }

        
        [Test]
        public void CanRoundTripConvert()
        {
            var document = _writer.Write(_advert);

            var roundTripped = _reader.Read(document);
            
            var differences = Compare.Properties(_advert, roundTripped);

            Assert.That(differences, Is.Empty);
        }
    }
}