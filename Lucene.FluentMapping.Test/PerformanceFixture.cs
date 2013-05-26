using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Lucene.FluentMappings.Demo;
using NUnit.Framework;

namespace Lucene.FluentMapping.Test
{
    [TestFixture]
    public class PerformanceFixture
    {
        private IDocumentMapper<Advert> _mapper;
        private Advert _advert;

        public IEnumerable<int> TestCases 
        {
            get 
            {
                yield return 100;
                yield return 1000;
                yield return 10000;
                yield return 100000;
                //yield return 1000000;
                //yield return 10000000;
            }
        }

        [SetUp]
        public void SetUp()
        {
            _mapper = DocumentMapper.For<Advert>();

            _advert = Example.Advert();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void DocumentMapperShouldBeFastConvertingToDucment(int iterations)
        {
            var elapsed = Time(() =>
                {
                    for (var i = 0; i < iterations; i++)
                        _mapper.Convert(_advert);
                });

            Debug.WriteLine("created {0} documents in {1}", iterations, elapsed);

            Assert.Pass();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void DocumentMapperShouldBeFastConvertingToDucmentInParallel(int iterations)
        {
            var elapsed = Time(() => Parallel.For(0, iterations, _ => _mapper.Convert(_advert)));

            Debug.WriteLine("created {0} documents in {1} (parallel)", iterations, elapsed);

            Assert.Pass();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void DocumentMapperShouldBeFastConvertingFromDucment(int iterations)
        {
            var document = _mapper.Convert(_advert);

            var elapsed = Time(() =>
                {
                    for (var i = 0; i < iterations; i++)
                        _mapper.Convert(document);
                });
            
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