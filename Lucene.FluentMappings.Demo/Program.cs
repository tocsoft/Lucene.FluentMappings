using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Lucene.FluentMapping;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace Lucene.FluentMappings.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var iterations = 100000;
            
            Console.WriteLine("Starting");

            // write documents created from POCOs

            CreateDocuments(Example.Adverts(iterations));
            WriteDocuments(Example.Adverts(iterations));

            // get POCOs from documents

            var writer = CreateIndexWriter();
            Example.Adverts(iterations).ToDocuments(writer.AddDocument);

            ReadDocuments(writer.GetReader());
            writer.Dispose();
            
            Console.WriteLine("Finished");

            Console.ReadLine();
        }
        
        private static void ReadDocuments(IndexReader reader)
        {
            IList<Advert> adverts = null;
            
            var elapsed = Time(() =>
                {
                    adverts = Enumerable.Range(0, reader.MaxDoc).Select(reader.Document).ToList<Advert>();
                });

            Console.WriteLine("extracted from {0} documents in {1}", adverts.Count, elapsed);
        }

        private static void CreateDocuments(IEnumerable<Advert> adverts)
        {
            var docs = 0;

            var elapsed = Time(() => adverts.ToDocuments(_ => Interlocked.Increment(ref docs)));

            Console.WriteLine("wrote to {0} documents in {1}", docs, elapsed);
        }

        private static void WriteDocuments(IEnumerable<Advert> adverts)
        {
            using (var indexWriter = CreateIndexWriter())
            {
                var elapsed = Time(() => adverts.ToDocuments(indexWriter.AddDocument));

                Console.WriteLine("indexed {0} documents in {1}", indexWriter.MaxDoc(), elapsed);
            }
        }

        private static TimeSpan Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();

            action();

            return stopwatch.Elapsed;
        }

        public static IndexWriter CreateIndexWriter()
        {
            return new IndexWriter(new RAMDirectory(), new SimpleAnalyzer(), true, IndexWriter.MaxFieldLength.UNLIMITED);
        }
    }
}
