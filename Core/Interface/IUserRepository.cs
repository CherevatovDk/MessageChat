using Core.Models;

namespace Core.Interface;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int id);
}