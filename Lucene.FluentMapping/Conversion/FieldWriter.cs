using System;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class FieldWriter
    {
        public static FieldWriter<TField, TSource, TProp>
            For<TField, TSource, TProp>(TField field, Func<TSource, TProp> getValue, Action<TField, TProp> apply)
            where TField : IFieldable
        {
            return new FieldWriter<TField, TSource, TProp>(field, getValue, apply);
        }
    }
    
    public class FieldWriter<TField, TSource, TProp> : IFieldWriter<TSource>
        where TField : IFieldable
    {
        private readonly Func<TSource, TProp> _getValue;
        private readonly Action<TField, TProp> _apply;
        private readonly TField _field;

        public IFieldable Field
        {
            get { return _field; }
        }

        public FieldWriter(TField field, Func<TSource, TProp> getValue, Action<TField, TProp> apply)
        {
            _getValue = getValue;
            _field = field;
            _apply = apply;
        }

        public void WriteValueFrom(TSource instance)
        {
            _apply(_field, _getValue(instance));
        }
    }
}