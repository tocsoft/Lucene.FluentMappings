using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lucene.FluentMapping.Configuration;
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

        /// <summary>
        /// Converts each <see cref="Document"/> to an instance of <typeparam name="TResult">TResult</typeparam>,
        /// using the type's default constructor.
        /// </summary>
        public static IList<TResult> ToList<TResult>(this IEnumerable<Document> documents)
            where TResult : new()
        {
            return ToList(documents, () => new TResult());
        }

        /// <summary>
        /// Converts each <see cref="Document"/> to an instance of <typeparam name="TResult">TResult</typeparam>, 
        /// using the supplied constructor delegate.
        /// </summary>
        public static IList<TResult> ToList<TResult>(this IEnumerable<Document> documents, Func<TResult> constructor)
        {
            var reader = GetDocumentReader(constructor);

            return documents
                .Select(reader.Read)
                .ToList();
        }

        /// <summary>
        /// Writes each instance to a document and calls the supplied delegate.
        /// </summary>
        public static void ToDocuments<TMapped>(this IEnumerable<TMapped> instances, Action<Document> documentAction)
        {
            var writer = GetDocumentWriter<TMapped>();

            foreach (var instance in instances)
                documentAction(writer.UpdateFrom(instance));
        }
        
        /// <summary>
        /// Gets a <see cref="DocumentWriter{TMapped}"/> instance for the specified type.
        /// </summary>
        public static DocumentWriter<TMapped> GetDocumentWriter<TMapped>()
        {
            var mappings = GetMappings<TMapped>();

            return new DocumentWriter<TMapped>(mappings);
        }

        /// <summary>
        /// Gets a <see cref="DocumentReader{TResult}"/> instance for the specified type.
        /// </summary>
        public static DocumentReader<TResult> GetDocumentReader<TResult>(Func<TResult> constructor)
        {
            var mappings = GetMappings<TResult>();

            return new DocumentReader<TResult>(constructor, mappings);
        }

        private static IEnumerable<IFieldMap<TMapped>> GetMappings<TMapped>()
        {
            return MappingFactory<TMapped>.GetMappings(_specifiedMappingSource);
        }
    }
}