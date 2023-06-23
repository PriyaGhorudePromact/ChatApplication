namespace ChatAppBackend.Model
{
    public class ResponseMessage
    {
        public string senderId { get; set; }
        public string receiverId { get; set; }
        public string content { get; set; }
        public DateTime timestamp { get; set; }
    }
}
