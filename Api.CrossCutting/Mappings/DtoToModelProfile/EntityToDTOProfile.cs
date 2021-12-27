using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings.DtoToModelProfile
{
    public class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        {
            CreateMap<UserDTO, UserEntity>()
                               .ReverseMap();


            CreateMap<UserCreateResultDTO, UserEntity>()
                                           .ReverseMap();


            CreateMap<UserUpdateResultDTO, UserEntity>()
                                           .ReverseMap();


        }
    }
}
