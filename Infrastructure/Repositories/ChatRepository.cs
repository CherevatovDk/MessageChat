using Core.Interface;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _context;

    public ChatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Chat> GetChatByIdAsync(int id)
    {
        return await _context.Chats
            .Include(c => c.CreatedByUser)
            .Include(c => c.Users)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Chat>> GetAllChatsAsync()
    {
        return await _context.Chats
            .Include(c => c.CreatedByUser)
            .Include(c => c.Users)
            .ToListAsync();
    }

    public async Task<Chat> AddChatAsync(Chat chat)
    {
        _context.Chats?.Add(chat);
        await _context.SaveChangesAsync();
        return chat;
    }

    public async Task<Chat> UpdateChatAsync(Chat chat)
    {
        _context.Entry(chat).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return chat;
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task DeleteChatAsync(int id)
    {
        if (_context.Chats != null)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat != null)
            {
                _context.Chats.Remove(chat);
                await _context.SaveChangesAsync();
            }
        }
    }
}