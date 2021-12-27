using Api.Domain.Entities;
using Api.Domain.Models.User;
using AutoMapper;

namespace Api.CrossCutting.Mappings.DtoToModelProfile
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<UserModel, UserEntity>()
                                 .ReverseMap();
        }
    }
}
