using Microsoft.AspNetCore.Identity;
using RealEstate.Core.Entities;

namespace RealEstate.Application.Helpers
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(new User { }, password);
            return hashedPassword;
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(new User { }, hashedPassword, providedPassword);
            return passwordVerificationResult == PasswordVerificationResult.Success;
        }
    }
}
