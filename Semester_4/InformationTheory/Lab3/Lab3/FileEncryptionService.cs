using System.IO;
using System.Numerics;

namespace RSA_WPF
{
    public static class FileEncryptionService
    {
        public const string EncryptedExtension = ".rsa";

        public static byte[] ReadFileBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public static void WriteFileBytes(string filePath, byte[] data)
        {
            File.WriteAllBytes(filePath, data);
        }

        public static void SaveEncryptedToFile(string outputPath, List<BigInteger> encryptedValues)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                foreach (var val in encryptedValues)
                {
                    ushort toWrite = (ushort)(val % 65536); 
                    writer.Write(toWrite);
                }
            }
        }

        public static List<BigInteger> LoadEncryptedFromFile(string filePath)
        {
            List<BigInteger> result = new List<BigInteger>();
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    ushort block = reader.ReadUInt16();
                    result.Add(new BigInteger(block));
                }
            }
            return result;
        }

        public static string EncryptedNumbersToString(List<BigInteger> numbers)
        {
            return string.Join(" ", numbers);
        }
    }
}