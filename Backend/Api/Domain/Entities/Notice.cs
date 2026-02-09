using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Notice
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public Location Location { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Notice() { }

        [JsonConstructor]
        public Notice(
         Guid id,
         string title,
         string content,
         Location location,
         DateTime createdAt)
        {
            Id = id;
            Title = title;
            Content = content;
            Location = location;
            CreatedAt = createdAt;
        }


        public Notice(string title, string content, Location location)
        {
            Id = Guid.NewGuid();
            Title = title;
            Content = content;
            Location = location;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
