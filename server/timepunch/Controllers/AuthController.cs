using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using timepunch.Models;
using timepunch.Services;

namespace timepunch.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<UserModelRO> Create(UserModel user)
        {
            UserModelRO userRO;
            try { userRO = _auth.CreateUser(user); } catch(System.Exception e) {
                userRO = new UserModelRO { error = e.Message };
            }
            return userRO;
        }

        [HttpPost]
        [Route("login")]
        async public Task<ActionResult<UserModelRO>> Login(UserModel user)
        {
            UserModelRO userRO;
            var loginUser = Task.Run(() => _auth.LoginUser(user));
            try { userRO = await loginUser; } catch(System.Exception e) {
                userRO = new UserModelRO { error = e.Message };
            }
            return userRO;
        }
    }
}