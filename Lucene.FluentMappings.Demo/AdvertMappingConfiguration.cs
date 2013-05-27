using System.Collections.Generic;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMappings.Demo
{
    public class AdvertMappingConfiguration : IMappingConfiguration<Advert>
    {
        public IEnumerable<IFieldMap<Advert>> BuildMappings()
        {
            return Mappings.For<Advert>(a =>
                {
                    a.Map(x => x.Id);

                    a.Map(x => x.Description)
                     .Configure(o => o.TermVector = Field.TermVector.WITH_OFFSETS);

                    a.Map(x => x.Title);

                    a.Map(x => x.Colour);

                    a.Map(x => x.Price);

                    a.Map(x => x.Height);

                    a.Map(x => x.Width);

                    a.Map(x => x.Depth);

                    a.Map(x => x.CategoryName)
                     .Configure(o => o.Index = Field.Index.NO);

                    a.Map(x => x.CategoryId);

                    a.Map(x => x.Uri)
                     .Configure(o => o.Index = Field.Index.NO);

                    a.Map(x => x.PrimaryImageUri)
                     .Configure(o => o.Index = Field.Index.NO);

                    a.Map(x => x.Expiry)
                     .Configure(o => o.Index = true);

                    a.Map(x => x.DiscountPercentage)
                     .Configure(o => o.Index = false);

                    a.Map(x => x.DiscountExpiry)
                     .Configure(o => o.Index = false);
                });
        }
    }
}