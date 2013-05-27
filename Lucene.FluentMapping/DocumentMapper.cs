using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
        
        public static IList<TResult> ToList<TResult>(this IEnumerable<Document> documents, int? parallelism = null)
            where TResult : new()
        {
            return ToList(documents, () => new TResult(), parallelism);
        }

        public static IList<TResult> ToList<TResult>(this IEnumerable<Document> documents, Func<TResult> constructor, int? parallelism = null)
        {
            var reader = GetDocumentReader(constructor);
            
            var converted = documents.Select(reader.Read);
            
            if (parallelism.HasValue)
                converted = converted.AsParallel().WithDegreeOfParallelism(parallelism.Value);

            return converted.ToList();
        }

        public static void ToDocuments<TMapped>(this IEnumerable<TMapped> instances, Action<Document> documentAction)
        {
            var writer = GetDocumentWriter<TMapped>();

            foreach (var instance in instances)
            {
                writer.UpdateFrom(instance);

                documentAction(writer.Document);
            }
        }

        public static void ToDocumentsParallelEx<TMapped>(this IEnumerable<TMapped> instances, Action<Document> documentAction)
        {
            Parallel.ForEach(instances, GetDocumentWriter<TMapped>, (instance, _, writer) =>
                {
                    writer.UpdateFrom(instance);
                    documentAction(writer.Document);
                    return writer;
                }, _ => { });
        }

        private static DocumentWriter<TMapped> GetDocumentWriter<TMapped>()
        {
            var mappings = MappingFactory.GetMappings<TMapped>(_specifiedMappingSource);

            return new DocumentWriter<TMapped>(mappings);
        }

        private static DocumentReader<TResult> GetDocumentReader<TResult>(Func<TResult> constructor)
        {
            var mappings = MappingFactory.GetMappings<TResult>(_specifiedMappingSource);

            return new DocumentReader<TResult>(constructor, mappings);
        }
    }
}