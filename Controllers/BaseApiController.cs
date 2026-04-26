using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace CodeLingo.Backend.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return 0;
            }

            return int.Parse(userIdClaim.Value);
        }
    }
}