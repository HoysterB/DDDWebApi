using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<UserDTO> Get(Guid id);
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserCreateResultDTO> Post(UserDTOCreate entity);
        Task<UserUpdateResultDTO> Put(UserDTOUpdate entity);
        Task<bool> Delete(Guid id);
    }
}
