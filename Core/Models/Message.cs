namespace Core.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Timestamp { get; set; }
    }
}