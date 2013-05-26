using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lucene.FluentMapping;
using Lucene.Net.Documents;

namespace Lucene.FluentMappings.Demo
{
    class Program
    {
        private static IDocumentMapper<Advert> _mapper;
        private static Advert _advert;
        private static Document _document;
        private static IList<Document> _documents;

        static void Main(string[] args)
        {
            _mapper = DocumentMapper.For<Advert>();
            _advert = Example.Advert();
            _document =  _mapper.Convert(_advert);
            
            var iterations = 1000000;

            _documents = Enumerable
                .Range(0, iterations)
                .Select(_ => _document).ToList();

            ReadDocuments();
            //ReadDocumentsInParallel();
            //WriteDocuments(iterations);
            //WriteDocumentsInParallel(iterations);
        }

        private static void ReadDocuments()
        {
            IList<Advert> results = null;

            var elapsed = Time(() =>
                {
                    results = _documents.Select(_mapper.Convert).ToList();
                });
            
            Console.WriteLine("extracted from {0} documents in {1}", results.Count, elapsed);
        }

        private static void ReadDocumentsInParallel()
        {
            IList<Advert> results = null;

            var elapsed = Time(() =>
            {
                results = _documents.ToList<Advert>();
            });

            Console.WriteLine("extracted from {0} documents in {1} (parallel)", results.Count, elapsed);
        }

        private static void WriteDocuments(int iterations)
        {
            var documents = new List<Document>(iterations);

            var elapsed = Time(() =>
                {
                    for (var i = 0; i < iterations; i++)
                        documents.Add(_mapper.Convert(_advert));
                });

            Console.WriteLine("created {0} documents in {1}", documents.Count, elapsed);
        }

        private static void WriteDocumentsInParallel(int iterations)
        {
            List<Document> documents = null;

            var elapsed = Time(() =>
            {
                documents = Enumerable.Range(0, iterations)
                    .AsParallel()
                    .Select(_ => _mapper.Convert(_advert))
                    .ToList();
            });

            Console.WriteLine("created {0} documents in {1} (parallel)", documents.Count, elapsed);
        }

        private static TimeSpan Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();

            action();

            return stopwatch.Elapsed;
        }   
    }
}
