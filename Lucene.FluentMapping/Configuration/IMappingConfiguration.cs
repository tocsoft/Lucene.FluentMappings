using System.Collections.Generic;

namespace Lucene.FluentMapping.Configuration
{
    public interface IMappingConfiguration<T>
    {
        IEnumerable<IFieldMap<T>> BuildMappings();
    }
}