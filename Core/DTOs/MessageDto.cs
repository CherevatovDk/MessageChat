namespace Core.DTOs;

public class MessageDto
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public string? Username { get; set; }
    public DateTime Timestamp { get; set; }
}