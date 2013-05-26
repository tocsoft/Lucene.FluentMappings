using System.Collections.Generic;
using Lucene.FluentMapping.Configuration;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMappings.Demo
{
    public class AdvertMappingConfiguration : IMappingConfiguration<Advert>
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