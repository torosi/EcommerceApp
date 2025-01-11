using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Data.Entities
{
    public class ApplicationUserEntity : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; } = string.Empty;
        public string? AddressLine3 { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? PostCode { get; set; } = string.Empty;

    }
}
