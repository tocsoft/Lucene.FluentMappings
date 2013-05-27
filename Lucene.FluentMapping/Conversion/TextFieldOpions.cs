using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class TextFieldOpions
    {
        public Field.Index Index { get; set; }
        public Field.Store Store { get; set; }
        public Field.TermVector TermVector { get; set; }

        public TextFieldOpions()
        {
            Index = Field.Index.ANALYZED;
            Store = Field.Store.NO;
            TermVector = Field.TermVector.NO;
        }
    }
}