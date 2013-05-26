using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.FluentMapping.Configuration;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping
{
    public static class DocumentMapper
    {
        public static IDocumentMapper<TResult> For<TResult>()
            where TResult : new()
        {
            return For(() => new TResult());
        }

        public static IDocumentMapper<TResult> For<TResult>(Func<TResult> constructor)
        {
            return new DocumentMapper<TResult>(constructor);
        }

        public static IList<TResult> ToList<TResult>(this IEnumerable<Document> documents)
            where TResult : new()
        {
            return Convert(documents, For<TResult>());
        }

        public static IList<TResult> ToList<TResult>(this IEnumerable<Document> documents, Func<TResult> constructor)
        {
            return Convert(documents, For(constructor));
        }

        public static void Stream<TMapped>(this IEnumerable<TMapped> instances, Action<Document> documentAction)
        {
            // TODO re-use same document + fields
        }

        private static IList<TResult> Convert<TResult>(IEnumerable<Document> documents, IDocumentMapper<TResult> mapper) 
        {
            return documents
                .AsParallel()
                .Select(mapper.Convert)
                .ToList();
        }
    }

    public class DocumentMapper<T> : IDocumentMapper<T>
    {
        private readonly DocumentReader<T> _reader;
        private readonly DocumentWriter<T> _writer; 
        
        public DocumentMapper(Func<T> create)
        {
            var mappings = MappingFactory.GetMappings<T>().ToList();

            _reader = new DocumentReader<T>(create, mappings);
            _writer = new DocumentWriter<T>(mappings);
        }
        
        public Document Convert(T source)
        {
            return _writer.Write(source);
        }
        
        public T Convert(Document source)
        {
            return _reader.Read(source);
        }

    }
}