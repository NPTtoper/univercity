namespace Lab6.Serialization
{
    public interface ICompressor
    {
        string Compress(string data);
        string Decompress(string data);
    }
}