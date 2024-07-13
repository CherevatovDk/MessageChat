using Core.Models;

namespace Core.Interface;

public interface IChatRepository
{
    Task<Chat> GetChatByIdAsync(int id);
    Task<IEnumerable<Chat>> GetAllChatsAsync();
    Task<Chat> AddChatAsync(Chat chat);
    Task<Chat> UpdateChatAsync(Chat chat);
    Task DeleteChatAsync(int id);
    Task<bool> SaveChangesAsync();
}