using AutoMapper;
using Core.DTOs;
using Core.Models;

namespace MessageChat.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Chat, ChatDto>()
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.Username))
            .ForMember(dest => dest.Usernames, opt => opt.MapFrom(src => src.Users.Select(u => u.Username)));

        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
    }

}