using System.Text.Json;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Infrastructure.Messaging
{
    public class SystemJsonSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public T Deserialize<T>(string content)
        {
            return JsonSerializer.Deserialize<T>(content);
        }
    }
}
