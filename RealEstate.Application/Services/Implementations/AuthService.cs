using RealEstate.Core.Entities;
using RealEstate.Application.Helpers;
using Microsoft.Extensions.Configuration;
using RealEstate.DataAccess.Dapper;
using Azure.Core;
using RealEstate.Application.Dto.Auth;

namespace RealEstate.Application.Services.Implementations
{

    public class AuthService : IAuthService
    {
        private readonly ISqlDataAccess _db;
        private readonly IConfiguration _config;

        public AuthService(ISqlDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<bool> RegisterUserAsync(CreateUserDto dto)
        {
            var userExists = (await _db.LoadDataAsync<CreateUserDto, dynamic>(
                "dbo.spUser_GetByEmail",
                new { Email = dto.Email })).FirstOrDefault();

            if (userExists == null)
            {
                var hashedPassword = PasswordHelper.HashPassword(dto.Password);

                var success = await _db.SaveDataAsync("dbo.spAuth_InsertUser", new { FullName = dto.FullName, Email = dto.Email, ContactNumber = dto.ContactNumber, Password = hashedPassword, Gender = dto.Gender, RoleID = dto.RoleID, City = dto.City });
                if (!success)
                {
                    return false;
                }

                return true;
            }
            else
            {
                throw new Exception("User already exists");
            }
        }

        public async Task<TokenResponseDto?> LoginUserAsync(UserLoginDto request, string ip, string userAgent)
        {
            var user = (await _db.LoadDataAsync<User, dynamic>(
                "dbo.spUser_GetByEmail",
                new { Email = request.Email })).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var passwordVerificationResult = PasswordHelper.VerifyPassword(user.Password, request.Password);
            if (!passwordVerificationResult)
            {
                return null;
            }

            var accessToken = TokenHelper.CreateToken(user, _config);

            var refreshToken = TokenHelper.GenerateRefreshToken();
            var hashedToken = TokenHelper.HashToken(refreshToken);

            await _db.SaveDataAsync("dbo.spRefreshTokens_Insert", new
            {
                UserID = user.ID,
                TokenHash = hashedToken,
                ExpiresAtUtc = request.RememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddDays(1),
                CreatedAtUtc = DateTime.UtcNow,
                CreatedByIp = ip,
                UserAgent = userAgent
            });

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponseDto?> RefreshAsync(string refreshToken, string ip, string userAgent)
        {
            var existing = await ValidateRefreshTokenAsync(refreshToken, ip);
            if (existing is null)
                return null;

            var newRefreshToken = TokenHelper.GenerateRefreshToken();
            var newHashedToken = TokenHelper.HashToken(newRefreshToken);

            await _db.SaveDataAsync("dbo.spRefreshTokens_Rotate", new
            {
                TokenHash = existing.TokenHash,
                ReplacedByTokenHash = newHashedToken,
                RevokedByIp = ip,
                RevokedAtUtc = DateTime.UtcNow
            });

            await _db.SaveDataAsync("dbo.spRefreshTokens_Insert", new
            {
                @UserID = existing.UserID,
                @TokenHash = newHashedToken,
                @ExpiresAtUtc = existing.ExpiresAtUtc,
                @CreatedAtUtc = DateTime.UtcNow,
                @CreatedByIp = ip,
                @UserAgent = userAgent
            });

            var user = (await _db.LoadDataAsync<User, dynamic>("dbo.spUser_GetByID", new { ID = existing.UserID })).FirstOrDefault();
            var newAccessToken = TokenHelper.CreateToken(user, _config);

            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task Logout(string refreshToken, string ip)
        {
            var hashedToken = TokenHelper.HashToken(refreshToken);

            var existing = (await _db.LoadDataAsync<RefreshToken, dynamic>("dbo.spRefreshTokens_GetByHash", new { TokenHash = hashedToken })).FirstOrDefault();
            if (existing == null) return;

            if (!existing.IsRevoked)
            {
                await _db.SaveDataAsync("dbo.spRefreshTokens_Revoke", new
                {
                    TokenHash = hashedToken,
                    RevokedAtUtc = DateTime.UtcNow,
                    RevokedByIp = ip
                });
            }
        }


        private async Task<RefreshToken?> ValidateRefreshTokenAsync(string refreshToken, string ip)
        {
            var hashedToken = TokenHelper.HashToken(refreshToken);

            var existing = (await _db.LoadDataAsync<RefreshToken, dynamic>("dbo.spRefreshTokens_GetByHash", new { TokenHash = hashedToken })).FirstOrDefault();
            if (existing == null)
                return null;

            if (existing.IsRevoked)
            {
                await _db.SaveDataAsync("dbo.spRefreshTokens_RevokeAllByUser", new
                {
                    UserID = existing.UserID,
                    RevokedAtUtc = DateTime.UtcNow,
                    RevokedByIp = ip
                });
                return null;
            }

            if (!existing.IsActive)
                return null;

            return existing;
        }

    }
}
