namespace Core.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Users { get; set; }
        
    }
}