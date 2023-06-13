using Newtonsoft.Json;

using System.Dynamic;

namespace Coditech.Admin.Utilities
{
    public class ApplicationSessionConfiguration
    {
        /// <summary>
        /// Serializes the given class into a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the class to serialize.</typeparam>
        /// <param name="tClass">The class to serialize.</param>
        /// <returns>A JSON string representing the given class.</returns>
        public string GetSerializedData<T>(T tClass) => JsonConvert.SerializeObject(tClass, Formatting.None);

        /// <summary>
        /// Deserializes the given session string into the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the session string into.</typeparam>
        /// <param name="sessionString">The session string to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public T GetDeSerializeData<T>(string sessionString)
        {
            if (!string.IsNullOrEmpty(sessionString))
                return JsonConvert.DeserializeObject<T>(sessionString)!;

            return default(T)!;
        }


        /// <summary>
        /// Deserializes a JSON string into a list of ExpandoObjects and returns them as a list of dynamic objects.
        /// </summary>
        /// <param name="sessionString">The JSON string to deserialize.</param>
        /// <returns>A list of dynamic objects.</returns>
        public List<dynamic> GetDeSerializeExpandoData(string sessionString)
        {
            if (!string.IsNullOrEmpty(sessionString))
            {
                List<ExpandoObject> list = JsonConvert.DeserializeObject<List<ExpandoObject>>(sessionString);
                return list.Select(d => d as dynamic).ToList();
            }
            return default(List<dynamic>);
        }
    }
}
