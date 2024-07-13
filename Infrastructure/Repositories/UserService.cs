using Core.Interface;

namespace Infrastructure.Repositories;

public class UserService:IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> UserExistsAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user != null;
    }
    
}