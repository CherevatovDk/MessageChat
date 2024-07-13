using Core.Interface;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await  _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    }
}