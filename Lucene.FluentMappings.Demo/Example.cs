using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.FluentMapping;
using Lucene.Net.Documents;

namespace Lucene.FluentMappings.Demo
{
    public static class Example
    {
        public static IEnumerable<Advert> Adverts(int count)
        {
            return Instances(Advert(), count).ToList();
        }

        public static Advert Advert()
        {
            return new Advert(123, "bookcases")
                {
                    Colour = "natural oak",
                    Depth = 200,
                    Height = 2000,
                    Width = 600,
                    Description = "a very nice bookcase",
                    Title = "oak bookcase",
                    Id = 999,
                    Price = 400,
                    PrimaryImageUri = new Uri("http://www.example.com/foo"),
                    Uri = new Uri("http://www.example.com/bar"),
                    DiscountExpiry = DateTime.Today.AddDays(10),
                    DiscountPercentage = 16.66M,
                    Expiry = DateTime.Today.AddMonths(3)
                };
        }

        public static IEnumerable<T> Instances<T>(T model, int count)
        {
            return Instances(() => model, count);
        }

        public static IEnumerable<T> Instances<T>(Func<T> constructor, int count)
        {
            return Enumerable.Range(0, count).Select(_ => constructor());
        }

        public static IEnumerable<Document> Documents(int count)
        {
            var advert = Advert();

            var document = Convert(advert);

            return Instances(document, count);
        }

        private static Document Convert(Advert advert)
        {
            Document document = null;

            new[] {advert}.ToDocuments(d => document = d);

            return document;
        }
    }
}