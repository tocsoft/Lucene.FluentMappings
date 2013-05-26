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
        private static IEnumerable<Document> _documents;
        private static IEnumerable<Advert> _adverts;

        static void Main(string[] args)
        {
            _mapper = DocumentMapper.For<Advert>();
            _advert = Example.Advert();
            _document =  _mapper.Convert(_advert);
            
            var iterations = 50000;

            _documents = Copy(_document, iterations);
            _adverts = Copy(_advert, iterations);
            
            ReadDocuments();
            //ReadDocumentsInParallel();
            WriteDocuments();
            //WriteDocumentsInParallel(iterations);

            Console.ReadLine();
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

        private static void WriteDocuments()
        {
            IList<Document> documents = null;

            var elapsed = Time(() =>
                {
                    documents = _adverts.Select(x => _mapper.Convert(x)).ToList();
                });

            Console.WriteLine("created {0} documents in {1}", documents.Count, elapsed);
        }

        private static void WriteDocumentsInParallel()
        {
            IList<Document> documents = null;

            var elapsed = Time(() =>
            {
                documents = _adverts
                    .AsParallel()
                    .Select(x => _mapper.Convert(x))
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

        private static IEnumerable<T> Copy<T>(T source, int iterations)
        {
            return Enumerable
                .Range(0, iterations)
                .Select(_ => source);
        }
    }
}
