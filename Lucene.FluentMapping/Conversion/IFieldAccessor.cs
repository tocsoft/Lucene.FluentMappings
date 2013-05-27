namespace Lucene.FluentMapping.Conversion
{
    public interface IFieldAccessor<TField, TStored>
    {
        void SetValue(TField field, TStored value);

        TStored GetValue(TField field);
    }
}