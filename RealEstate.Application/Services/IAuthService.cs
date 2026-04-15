using RealEstate.Application.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(CreateUserDto dto);
        Task<TokenResponseDto?> LoginUserAsync(UserLoginDto dto,string ip,string userAgent);
        Task<TokenResponseDto?> RefreshAsync(string refreshToken, string ip, string userAgent);
        Task Logout(string refreshToken, string ip);
    }
}
