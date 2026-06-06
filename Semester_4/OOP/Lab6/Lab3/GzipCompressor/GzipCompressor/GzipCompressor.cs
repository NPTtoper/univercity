using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Lab3.Serialization;

namespace CompressionPlugin
{
    public class GzipCompressor : ICompressor
    {
        public string Compress(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public string Decompress(string data)
        {
            byte[] bytes = Convert.FromBase64String(data);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(gzip, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}