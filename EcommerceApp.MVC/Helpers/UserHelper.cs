using EcommerceApp.MVC.Helpers.Interfaces;
using System.Security.Claims;

namespace EcommerceApp.MVC.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string? GetUserId()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;

            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return null;
            }

            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                throw new Exception("User ID could not be found.");
            }

            return userIdClaim.Value;
        }
    }
}
