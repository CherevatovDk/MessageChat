namespace Core.DTOs;

public class ChatDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CreatedByUsername { get; set; }
    public List<string>? Usernames { get; set; }
    public int CreatedByUserId { get; set; }
   
}