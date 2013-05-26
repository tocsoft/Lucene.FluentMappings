using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lucene.FluentMapping.Configuration;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping
{
    public static class DocumentMapper
    {
        private static Assembly _specifiedMappingSource;

        public static void UseMappingsFromThisAssembly()
        {
            _specifiedMappingSource = Assembly.GetCallingAssembly();
        }

        public static void UseMappingsFromAssemblyContaining<T>()
        {
            UseMappingsFromAssembly(typeof(T).Assembly);
        }

        public static void UseMappingsFromAssembly(Assembly assembly)
        {
            _specifiedMappingSource = assembly;
        }
        
        public static IList<TResult> ToList<TResult>(this IEnumerable<Document> documents)
            where TResult : new()
        {
            return ToList(documents, () => new TResult());
        }

        public static IList<TResult> ToList<TResult>(this IEnumerable<Document> documents, Func<TResult> constructor)
        {
            return Convert(documents, constructor);
        }

        public static IDocumentMapper<TResult> For<TResult>()
            where TResult : new()
        {
            return For(() => new TResult());
        }

        public static IDocumentMapper<TResult> For<TResult>(Func<TResult> constructor)
        {
            var mappings = GetMappings<TResult>();

            return new DocumentMapper<TResult>(mappings, constructor);
        }

        public static void ToDocuments<TMapped>(this IEnumerable<TMapped> instances, Action<Document> documentAction)
        {
            var mappings = GetMappings<TMapped>();

            var writer = new DocumentWriter<TMapped>(mappings);

            // TODO can this be safely paralellised when IFieldMap becomes stateful?!

            instances
                .Select(writer.Write)
                .ToList()
                .ForEach(documentAction);
        }

        private static IList<TResult> Convert<TResult>(IEnumerable<Document> documents, Func<TResult> constructor)
        {
            var mappings = GetMappings<TResult>();

            var reader = new DocumentReader<TResult>(constructor, mappings);

            return documents
                .AsParallel()
                .Select(reader.Read)
                .ToList();
        }

        private static IEnumerable<IFieldMap<TResult>> GetMappings<TResult>()
        {
            var mappingSource = _specifiedMappingSource ?? typeof (TResult).Assembly;

            return MappingFactory.GetMappings<TResult>(mappingSource);
        }
    }

    public class DocumentMapper<T> : IDocumentMapper<T>
    {
        private readonly DocumentReader<T> _reader;
        private readonly DocumentWriter<T> _writer; 
        
        public DocumentMapper(IEnumerable<IFieldMap<T>> mappings, Func<T> create)
        {
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