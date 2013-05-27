using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class NumericFieldOptions
    {
        public Field.Store Store { get; set; }
        public bool Index { get; set; }
        public int Precision { get; set; }

        public NumericFieldOptions()
        {
            Store = Field.Store.NO;
            Index = true;
            Precision = 4;
        }

        public static NumericFieldOptions Stored()
        {
            return new NumericFieldOptions
                {
                    Store = Field.Store.YES
                };
        }
    }
}