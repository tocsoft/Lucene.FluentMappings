using System;

namespace Lucene.FluentMapping.Test
{
    public class Advert
    {
        public long Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        
        public string Colour { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? Depth { get; set; }
        
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public Uri Uri { get; set; }
        public Uri PrimaryImageUri { get; set; }
    }
}