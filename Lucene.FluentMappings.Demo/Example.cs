using System;

namespace Lucene.FluentMappings.Demo
{
    public static class Example
    {
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
                    Uri = new Uri("http://www.example.com/bar")
                };
        }
    }
}