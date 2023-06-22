namespace ChatAppBackend.Model
{
    public class Messages
    {
        public string Id { get; set; }
        public string senderId { get; set; }
        public string receiverId { get; set; }
        public string content { get; set; }
        public DateTime timestamp { get; set; }
    }
}
