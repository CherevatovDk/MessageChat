using Core.DTOs;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IChatService
    {
        Task<ChatDto> GetChatByIdAsync(int id);
        Task<IEnumerable<ChatDto>> GetAllChatsAsync();
        Task<ChatDto> AddChatAsync(ChatDto chatDto, int userId);
        Task<ChatDto> UpdateChatAsync(int id, ChatDto chatDto);
        Task DeleteChatAsync(int id, int userId);
        Task JoinChatAsync(int chatId, int userId);
       
    }
}