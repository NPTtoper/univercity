using System.Windows;
using Lab3.Serialization;

namespace XorPlugin
{
    public class XorProcessor : IDataProcessor, IConfigurableProcessor
    {
        public string Key { get; set; } = "SecretKey";
        public string Name => "XOR шифрование";

        public string ProcessBeforeSave(string data)
        {
            return XorEncrypt(data, Key);
        }

        public string ProcessAfterLoad(string data)
        {
            return XorEncrypt(data, Key);
        }

        public void Configure()
        {
            string key = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите ключ шифрования:",
                "Настройка XOR шифрования",
                Key);

            if (!string.IsNullOrEmpty(key))
            {
                Key = key;
            }
        }

        public string GetConfigurationInfo()
        {
            return $"Текущий ключ: {Key}";
        }

        private string XorEncrypt(string text, string key)
        {
            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                result[i] = (char)(text[i] ^ key[i % key.Length]);
            }
            return new string(result);
        }
    }
}