using System.IO;
using Newtonsoft.Json;

namespace ContextRecord.ContextSerializers
{
    /// <summary>
    /// The context serializer to JSON.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public class JsonContextSerializer<T> : IContextSerializer<T>
    {
        private readonly string path;

        public JsonContextSerializer(string path)
        {
            this.path = path;
        }

        public T? LoadContextData()
        {
            var data = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T?>(data);
        }

        public void SaveContextData(T? contextData)
        {
            //Check if the file path exists
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(contextData));
        }
    }
}
