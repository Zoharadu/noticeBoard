namespace Api.Requests
{
    public class CreateNoticeRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public LocationRequest Location { get; set; }
    }
}
