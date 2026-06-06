namespace Lab3.Serialization
{
    public interface IConfigurableProcessor
    {
        void Configure();
        string GetConfigurationInfo();
    }
}