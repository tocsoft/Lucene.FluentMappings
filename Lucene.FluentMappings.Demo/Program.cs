using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Lucene.FluentMapping;
using Lucene.Net.Documents;

namespace Lucene.FluentMappings.Demo
{
    class Program
    {
        private static IEnumerable<Document> _documents;
        private static IEnumerable<Advert> _adverts;

        static void Main(string[] args)
        {
            var iterations = 5000000;

            _adverts = Example.Adverts(iterations);
            _documents = Example.Documents(iterations);

            Console.WriteLine("Starting");

            //ReadDocuments();
            WriteDocuments();
            WriteDocumentsParallel();

            Console.WriteLine("Finished");

            Console.ReadLine();
        }
        
        private static void ReadDocuments()
        {
            IList<Advert> results = null;

            var elapsed = Time(() =>
            {
                results = _documents.ToList<Advert>();
            });

            Console.WriteLine("extracted from {0} documents in {1}", results.Count, elapsed);
        }

        private static void WriteDocuments()
        {
            IList<Document> documents = new List<Document>();

            var elapsed = Time(() => _adverts.ToDocuments(documents.Add));

            Console.WriteLine("wrote to {0} documents in {1}", documents.Count, elapsed);
        }

        private static void WriteDocumentsParallel()
        {
            var documents = new ConcurrentBag<Document>();

            var elapsed = Time(() => _adverts.ToDocumentsParallelEx(documents.Add));

            Console.WriteLine("wrote to {0} documents in {1} (p)", documents.Count, elapsed);
        }

        private static TimeSpan Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();

            action();

            return stopwatch.Elapsed;
        }
    }
}
