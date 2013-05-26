using System.Collections.Generic;
using System.Linq;
using Lucene.FluentMappings.Demo;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    public class DocumentMapperFixture
    {
        [Test]
        public void WhenWritingSeveralDocuments_EachDocumentMatchesItsSource()
        {
            var adverts = new[]
                {
                    new Advert {Title = "foo"},
                    new Advert {Title = "bar"},
                    new Advert {Title = "baz"},
                };

            var titles = new List<string>();

            adverts.ToDocuments(d => titles.Add(d.Get("Title")));

            Assert.That(titles, Is.SubsetOf(adverts.Select(x => x.Title)));
        }
    }
}