using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
            var iterations = 100000;

            _adverts = Example.Adverts(iterations);
            _documents = Example.Documents(iterations);

            Console.WriteLine("Starting");

            ReadDocuments();
            ReadDocumentsParallel(2);        // slower :(
            ReadDocumentsParallel(4);        // slower :(

            WriteDocuments();
            WriteDocumentsParallel();       // good boost

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

        private static void ReadDocumentsParallel(int parallelism)
        {
            IList<Advert> results = null;

            var elapsed = Time(() =>
            {
                results = _documents
                    .AsParallel()
                    .WithDegreeOfParallelism(parallelism)
                    .ToList<Advert>();
            });

            Console.WriteLine("extracted from {0} documents in {1} ({2} threads)", results.Count, elapsed, parallelism);
        }

        private static void WriteDocuments()
        {
            var docCount = 0;

            var elapsed = Time(() => _adverts.ToDocuments(_ => docCount++));

            Console.WriteLine("wrote to {0} documents in {1}", docCount, elapsed);
        }

        private static void WriteDocumentsParallel()
        {
            var docCount = 0;

            var elapsed = Time(() => _adverts.ToDocumentsParallelEx(_ => Interlocked.Increment(ref docCount)));

            Console.WriteLine("wrote to {0} documents in {1} (p)", docCount, elapsed);
        }

        private static TimeSpan Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();

            action();

            return stopwatch.Elapsed;
        }
    }
}
