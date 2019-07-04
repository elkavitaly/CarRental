using System.IO;
using Newtonsoft.Json;

namespace BusinessLayer.Utils
{
    public static class Util
    {
        public static T Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data);

        public static string Serialize<T>(T data) => JsonConvert.SerializeObject(data);

        public static string ReadStream(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var content = streamReader.ReadToEnd();
                return content;
            }
        }
    }
}