using System.Reflection;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Configuration
{
    public class StringFieldMap<T, TProperty> : IFieldMap<T>, IConfigurableFieldMap<TextFieldOptions> 
        where TProperty : class
    {
        private readonly TextFieldOptions _options = new TextFieldOptions();
        private readonly PropertyInfo _property;
        private readonly IFieldAccessor<Field, TProperty> _fieldAccessor;

        public TextFieldOptions Options
        {
            get { return _options; }
        }

        public StringFieldMap(PropertyInfo property, IFieldAccessor<Field, TProperty> fieldAccessor)
        {
            _property = property;
            _fieldAccessor = fieldAccessor;
        }
        
        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new Field(_property.Name, string.Empty, _options.Store, _options.Index, _options.TermVector)
                {
                    Boost = _options.Boost
                };

            var getter = _property.GetGetter<T, TProperty>();

            return FieldWriter.For(field, getter, _fieldAccessor.SetValue);
        }

        public IFieldReader<T> CreateFieldReader()
        {
            var setter = _property.GetSetter<T, TProperty>();

            return new FieldReader<T, TProperty>(GetValue, setter);
        }

        private TProperty GetValue(Document d)
        {
            var field = d.GetField(_property.Name);

            if (field == null || field.StringValue == null)
                return null;

            return _fieldAccessor.GetValue(field);
        }
    }
}