namespace ChatAppBackend.Model
{
    public class RetriveData
    {
        public string userId { get; set; }
        public DateTime before { get; set; }
        public int count { get; set; }
        public string sort { get; set; }
    }
}
