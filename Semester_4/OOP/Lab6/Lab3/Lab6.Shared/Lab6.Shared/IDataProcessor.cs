namespace Lab6.Serialization
{
    public interface IDataProcessor
    {
        string Name { get; }
        string ProcessBeforeSave(string data);
        string ProcessAfterLoad(string data);
    }
}