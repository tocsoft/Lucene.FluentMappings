using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Configuration
{
    public class TextFieldOptions
    {
        public Field.Index Index { get; set; }
        public Field.Store Store { get; set; }
        public Field.TermVector TermVector { get; set; }
        public float Boost { get; set; }

        public TextFieldOptions()
        {
            Index = Field.Index.ANALYZED;
            Store = Field.Store.YES;
            TermVector = Field.TermVector.NO;
            Boost = 1;
        }
    }
}