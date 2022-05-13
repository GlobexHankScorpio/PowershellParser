using Newtonsoft.Json;
using System.Text;

namespace PowershellParser
{
    static class JsonParser
    {
        public static T JsonToObject<T>(string json)
        {
            //Serialize json and deliver it
            T jsonConverted = JsonConvert.DeserializeObject<T>(json);

            return jsonConverted;
        }

        public static T JsonToObject<T>(byte[] resource)
        {
            //Serialize json and deliver it
            return JsonConvert.DeserializeObject<T>(EncodeResource(resource));
        }

        public static string ObjectToJson<T>(T resource)
        {
            //Serialize json and create powershell script
            string jsonConverted = JsonConvert.SerializeObject(resource);

            return jsonConverted;
        }

        private static string EncodeResource(byte[] resource)
        {
            return Encoding.ASCII.GetString(resource);
        }
    }
}
