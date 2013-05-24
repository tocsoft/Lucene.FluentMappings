using System.Collections.Generic;
using Lucene.FluentMapping.Configuration;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    [TestFixture]
    public class DocumentMapperFixture
    {
        private DocumentMapper<Advert> _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = DocumentMapper.For<Advert>();
        }

        [Test]
        public void CanCovertToDocument()
        {
            var document = _mapper.Convert(new Advert());

            Assert.That(document, Is.Not.Null);
        }

        [Test]
        public void CanCovertFromDocument()
        {
            var instance = _mapper.Convert(new Document());

            Assert.That(instance, Is.Not.Null);
        }
    }



    public class ListingResultMappingConfiguration : IMappingConfiguration<Advert>
    {
        public IEnumerable<IFieldMap<Advert>> BuildMappings()
        {
            return Build();
        }

        // resharper is wrong haha
        private static List<IFieldMap<Advert>> Build()
        {
            return Mappings.For<Advert>()
                   .Map(x => x.Id, indexed: true)
                   .Timestamp("Advert")
                   .Map(x => x.Description, indexed: true)
                   .Map(x => x.Title, indexed: true)
                   .Map(x => x.Colour, indexed: true)
                   .Map(x => x.Price, indexed: true)
                   .Map(x => x.Height, indexed: true)
                   .Map(x => x.Width, indexed: true)
                   .Map(x => x.Depth, indexed: true)
                   .Map(x => x.CategoryName)
                   .Map(x => x.CategoryId, indexed: true)
                   .Map(x => x.Uri)
                   .Map(x => x.PrimaryImageUri);
        }
    }
}