using System.Reflection;

namespace Lucene.FluentMapping.Test
{
    public class PropertyAndValue
    {
        public PropertyInfo Property { get; private set; }

        public object Value { get; private set; }
        
        public PropertyAndValue(PropertyInfo property, object value)
        {
            Property = property;
            Value = value;
        }

        public static PropertyAndValue From(PropertyInfo property, object instance)
        {
            return new PropertyAndValue(property, property.GetValue(instance, null));
        }
    }
}