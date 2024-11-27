using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Cache
{
    public class JsonHybridCacheSerializer<T> : IHybridCacheSerializer<T>
    {
        private readonly JsonSerializerOptions _options;

        public JsonHybridCacheSerializer()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public T Deserialize(ReadOnlySequence<byte> source)
        {
            if (source.IsSingleSegment)
            {
                return JsonSerializer.Deserialize<T>(source.First.Span, _options)!;
            }

            using var memoryStream = new MemoryStream();
            foreach (var segment in source)
            {
                memoryStream.Write(segment.Span);
            }
            memoryStream.Position = 0;

            return JsonSerializer.Deserialize<T>(memoryStream.ToArray(), _options)!;
        }

        public void Serialize(T value, IBufferWriter<byte> target)
        {
            using var writer = new Utf8JsonWriter(target);
            JsonSerializer.Serialize(writer, value, _options);
        }
    }
}
