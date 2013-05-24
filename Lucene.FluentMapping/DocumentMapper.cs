using System;
using System.Collections.Generic;
using Lucene.FluentMapping.Configuration;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping
{
    public static class DocumentMapper
    {
        public static DocumentMapper<TResult> For<TResult>()
            where TResult : new()
        {
            return For(() => new TResult());
        }

        public static DocumentMapper<TResult> For<TResult>(Func<TResult> constructor)
        {
            return new DocumentMapper<TResult>(constructor);
        }
    }

    public class DocumentMapper<T> : IDocumentMapper<T>
    {
        private readonly Func<T> _create;
        private readonly IEnumerable<IFieldMap<T>> _mappings;

        public DocumentMapper(Func<T> create)
        {
            _create = create;
            _mappings = MappingFactory.GetMappings<T>();
        }

        public Document Convert(T source)
        {
            var document = new Document();

            foreach (var mapping in _mappings)
            {
                var field = mapping.GetField(source);

                if (field != null)
                    document.Add(field);
            }

            return document;
        }
        
        public T Convert(Document source)
        {
            var instance = _create();

            foreach (var mapping in _mappings)
                mapping.ValueFrom(source).Apply(instance);

            return instance;
        }
    }
}