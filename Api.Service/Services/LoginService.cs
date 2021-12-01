using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _repository;
        private SigninConfigurations _signinConfiguration;
        private TokenConfiguration _tokenConfiguration;
        private IConfiguration _configuration { get; }

        public LoginService(IUserRepository repository,
                            SigninConfigurations signinConfiguration,
                            TokenConfiguration tokenConfiguration,
                            IConfiguration configuration)
        {
            _repository = repository;
            _signinConfiguration = signinConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }
        public async Task<object> FindByLogin(LoginDTO user)
        {
            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);

                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "falha ao autenticar"
                    };
                }
                else
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                        });

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);

                    return SuccessObject(createDate, expirationDate, token, user);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "falha ao autenticar"
                };
            }
        }
        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate,
                                        JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signinConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate,
                                    string token, LoginDTO user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                message = "Usuário logado com sucesso"
            };
        }
    }
}
