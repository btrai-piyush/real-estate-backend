using RealEstate.Application.Dto.Auth;
using RealEstate.Core.Entities;
using RealEstate.DataAccess.Dapper;

namespace RealEstate.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ISqlDataAccess _db;

        public UserService(ISqlDataAccess db) {
            _db = db;
        }
        public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
        {
            var user = (await _db.LoadDataAsync<UserResponseDto, dynamic>("dbo.spUser_GetByEmail", new { Email = email })).FirstOrDefault();
            return user;
        }
    }
}
