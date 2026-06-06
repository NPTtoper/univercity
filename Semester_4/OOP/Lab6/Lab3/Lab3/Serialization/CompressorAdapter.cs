namespace Lab3.Serialization
{
    public class CompressorAdapter : IDataProcessor, IConfigurableProcessor
    {
        private readonly ICompressor _compressor;

        public CompressorAdapter(ICompressor compressor)
        {
            _compressor = compressor;
        }

        public string Name => "Адаптер сжатия";

        public string ProcessBeforeSave(string data)
        {
            return _compressor.Compress(data);
        }

        public string ProcessAfterLoad(string data)
        {
            return _compressor.Decompress(data);
        }

        public void Configure()
        {
            System.Windows.MessageBox.Show("Сжатие GZip настроено автоматически");
        }

        public string GetConfigurationInfo()
        {
            return "Адаптер для GZip сжатия данных";
        }
    }
}