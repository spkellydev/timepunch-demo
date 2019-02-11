using Microsoft.AspNetCore.Mvc;
using timepunch.Models;
using timepunch.Services.User;

namespace timepunch.Controllers
{
    [Route("v1/api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _repo;
        public UserController(IUserService repo) { _repo = repo; }
        [HttpGet("profile")]
        public UserProfileModel CreateProfile()
        {
            /// temporary dummy route to better visualize
            return _repo.CreateProfile();
        }
    }
}