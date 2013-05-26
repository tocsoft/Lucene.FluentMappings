using System;
using System.Collections.Generic;
using System.Diagnostics;
using Lucene.FluentMapping;
using Lucene.Net.Documents;

namespace Lucene.FluentMappings.Demo
{
    class Program
    {
        private static IDocumentMapper<Advert> _mapper;
        
        private static IEnumerable<Document> _documents;
        private static IEnumerable<Advert> _adverts;

        static void Main(string[] args)
        {
            var iterations = 50000;

            _mapper = DocumentMapper.For<Advert>();
            _adverts = Example.Adverts(iterations);
            
            var document =  _mapper.Convert(Example.Advert());
            _documents = Example.Instances(document, iterations);
            
            ReadDocuments();
            WriteDocuments();

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

            Console.WriteLine("created {0} documents in {1}", documents.Count, elapsed);
        }
        
        private static TimeSpan Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();

            action();

            return stopwatch.Elapsed;
        }
    }
}
