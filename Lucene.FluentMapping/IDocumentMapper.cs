namespace Lucene.FluentMapping
{
    public interface IDocumentMapper<T>
    {
        // TODO consider method which sets fields on an existing Document
        // lucene docs say (said?) re-using document & field instance improves performance
    }
}