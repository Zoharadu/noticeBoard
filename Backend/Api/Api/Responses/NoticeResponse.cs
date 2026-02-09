namespace Api.Responses
{
    public class NoticeResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public LocationResponse Location { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
