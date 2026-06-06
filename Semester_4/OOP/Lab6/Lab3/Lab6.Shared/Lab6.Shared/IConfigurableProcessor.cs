namespace Lab6.Serialization
{
    public interface IConfigurableProcessor
    {
        void Configure();
        string GetConfigurationInfo();
    }
}