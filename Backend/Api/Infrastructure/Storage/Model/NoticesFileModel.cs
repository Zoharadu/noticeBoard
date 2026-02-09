using Domain.Entities;
using System.Text.Json.Serialization;

namespace Infrastructure.Storage.Model
{
    public class NoticesFileModel
    {
        [JsonPropertyName("notices")]
        public List<Notice> Notices { get; set; } = new();

        [JsonPropertyName("lastUpdatedAt")]
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
