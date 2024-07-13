namespace Core.Interface;

public interface IUserService
{
    Task<bool> UserExistsAsync(int userId);
}