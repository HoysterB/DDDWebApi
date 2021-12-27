using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        public UserService(
            IRepository<UserEntity> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDTO> Get(Guid id)
        {
            try
            {
                var entity = await _repository.SelectAsync(id);
                return _mapper.Map<UserDTO>(entity) ?? new UserDTO();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            try
            {
                var entityList = await _repository.SelectAsync();
                var dto = _mapper.Map<IEnumerable<UserDTO>>(entityList);
                return dto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<UserCreateResultDTO> Post(UserDTOCreate entity)
        {
            try
            {
                var model = _mapper.Map<UserModel>(entity);
                var userEntity = _mapper.Map<UserEntity>(model);
                var result = await _repository.InsertAsync(userEntity);

                return _mapper.Map<UserCreateResultDTO>(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<UserUpdateResultDTO> Put(UserDTOUpdate entity)
        {
            try
            {
                var model = _mapper.Map<UserModel>(entity);
                var userEntity = _mapper.Map<UserEntity>(model);
                var result = await _repository.UpdateAsync(userEntity);

                return _mapper.Map<UserUpdateResultDTO>(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}