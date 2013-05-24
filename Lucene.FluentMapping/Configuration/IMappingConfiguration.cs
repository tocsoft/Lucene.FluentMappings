using System.Collections.Generic;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public interface IMappingConfiguration<T>
    {
        IEnumerable<IFieldMap<T>> BuildMappings();
    }
}