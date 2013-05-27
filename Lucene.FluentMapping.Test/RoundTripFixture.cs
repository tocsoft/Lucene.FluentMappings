using System.Collections.Generic;
using System.Linq;
using Lucene.FluentMapping.Configuration;
using Lucene.FluentMappings.Demo;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    [TestFixture]
    public class RoundTripFixture
    {
        private DocumentReader<Advert> _reader;
        private DocumentWriter<Advert> _writer;
        
        public IEnumerable<Advert> Adverts
        {
            get
            {
                yield return new Advert();          // all default values
                yield return Example.Advert();      // all properties non-default
                // TODO try some odd values for datetime, decimal etc
            }
        }

        [SetUp]
        public void SetUp()
        {
            var mappings = MappingFactory.GetMappings<Advert>().ToList();
            _reader = new DocumentReader<Advert>(() => new Advert(), mappings);
            _writer = new DocumentWriter<Advert>(mappings);
        }
        
        [Test]
        [TestCaseSource("Adverts")]
        public void CanRoundTripConvert(Advert advert)
        {
            _writer.UpdateFrom(advert);

            var roundTripped = _reader.Read(_writer.Document);

            var differences = Compare.Properties(advert, roundTripped);

            Assert.That(differences, Is.Empty);
        }
    }
}