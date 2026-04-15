using RealEstate.Core.Enums;

namespace RealEstate.Core.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public Enums.UserRole RoleID { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime {  get; set; }
    }
}
