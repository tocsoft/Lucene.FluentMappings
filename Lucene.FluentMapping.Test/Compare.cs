using System.Collections.Generic;
using System.Linq;

namespace Lucene.FluentMapping.Test
{
    public static class Compare
    {
        public static List<string> Properties<T>(T first, T second)
        {
            var properties = typeof(T).GetProperties();

            var firstProperties = properties.Select(p => PropertyAndValue.From(p, first));
            var secondProperties = properties.Select(p => PropertyAndValue.From(p, second));

            return firstProperties
                .Zip(secondProperties, (p1, p2) => !Equals(p1.Value, p2.Value) ? new {p1,p2} : null)
                .Where(x => x != null)
                .Select(x => x.p1.Property.Name)
                .ToList();
        }
    }
}