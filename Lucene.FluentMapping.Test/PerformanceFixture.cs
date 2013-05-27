using System;
using System.Collections.Generic;
using System.Diagnostics;
using Lucene.FluentMappings.Demo;
using Lucene.Net.Documents;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    [TestFixture]
    public class PerformanceFixture
    {
        private Document _document;

        public IEnumerable<int> TestCases 
        {
            get 
            {
                yield return 100;
                yield return 1000;
                yield return 10000;
                //yield return 100000;
                //yield return 1000000;
                //yield return 10000000;
            }
        }

        [SetUp]
        public void SetUp()
        {
            new[] {Example.Advert()}.ToDocuments(d => _document = d);
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void DocumentMapperShouldBeFastConvertingToDucment(int iterations)
        {
            var adverts = Example.Adverts(iterations);

            var elapsed = Time(() => adverts.ToDocuments(d => { }));

            Debug.WriteLine("created {0} documents in {1}", iterations, elapsed);

            Assert.Pass();
        }
        
        [Test]
        [TestCaseSource("TestCases")]
        public void DocumentMapperShouldBeFastConvertingFromDucment(int iterations)
        {
            var documents = Example.Instances(_document, iterations);

            var elapsed = Time(() => documents.ToList<Advert>());
            
            Debug.WriteLine("extracted from {0} documents in {1}", iterations, elapsed);

            Assert.Pass();
        }

        private static TimeSpan Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();

            action();

            return stopwatch.Elapsed;
        }
    }
}