using AutoMapper;
using ChatAppBackend.Dto;
using ChatAppBackend.Model;
using System.Runtime.CompilerServices;

namespace ChatAppBackend.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserRegistration>();
            CreateMap<UserRegistration, User>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Messages, ResponseMessage>();
            CreateMap<ResponseMessage, Messages>();
        }
    }
}
