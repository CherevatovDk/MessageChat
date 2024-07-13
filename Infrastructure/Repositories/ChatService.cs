using AutoMapper;
using Core.DTOs;
using Core.Interface;
using Core.Models;

namespace Infrastructure.Repositories
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
     
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
      
        public ChatService(IChatRepository chatRepository,
            IUserService userService, IMapper mapper, IUserRepository userRepository)
        {
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
           
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ChatDto> GetChatByIdAsync(int id)
        {
            var chat = await _chatRepository.GetChatByIdAsync(id);
            if (chat == null)
            {
                throw new Exception("Chat not found");
            }

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<IEnumerable<ChatDto>> GetAllChatsAsync()
        {
            var chats = await _chatRepository.GetAllChatsAsync();
            return _mapper.Map<IEnumerable<ChatDto>>(chats);
        }
        

        public async Task<ChatDto> UpdateChatAsync(int id, ChatDto chatDto)
        {
            var chat = await _chatRepository.GetChatByIdAsync(id);
            if (chat == null)
            {
                throw new Exception("Chat not found");
            }
            
            chat.Name = chatDto.Name;
            await _chatRepository.UpdateChatAsync(chat);
            await _chatRepository.SaveChangesAsync();
            return _mapper.Map<ChatDto>(chat);
        }

        public async Task JoinChatAsync(int chatId, int userId)
        {
            var chat = await _chatRepository.GetChatByIdAsync(chatId);
            if (chat == null)
            {
                throw new Exception("Chat not found");
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (chat.Users.Contains(user))
            {
                throw new InvalidOperationException("User already in the chat");
            }

            chat.Users.Add(user);
            await _chatRepository.SaveChangesAsync();
        }

    
        public async Task<ChatDto> AddChatAsync(ChatDto chatDto, int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("User not found");
            var chat = new Chat
            {
                Name = chatDto.Name, CreatedByUserId = userId, CreatedByUser = user, Users = new List<User> { user }
            };
            await _chatRepository.AddChatAsync(chat);
            await _chatRepository.SaveChangesAsync();
            return _mapper.Map<ChatDto>(chat);
        }

        public async Task DeleteChatAsync(int id, int userId)
        {
            var chatToDelete = await _chatRepository.GetChatByIdAsync(id);
            if (chatToDelete == null) throw new Exception("Chat not found");
            if (chatToDelete.CreatedByUserId != userId)
                throw new UnauthorizedAccessException("No permission to delete this chat");
            await _chatRepository.DeleteChatAsync(id);
            await _chatRepository.SaveChangesAsync();
        }
    }
}
