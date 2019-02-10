using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using timepunch.Models;
using timepunch.Services.Authentication;

namespace timepunch.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _auth;
        public UserController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<IUserModelRO> Create(IUserModel user)
        {
            IUserModelRO userRO;
            try { userRO = _auth.CreateUser(user); } catch(System.Exception e) {
                userRO = new IUserModelRO { error = e.Message };
            }
            return userRO;
        }

        [HttpPost]
        [Route("login")]
        async public Task<ActionResult<IUserModelRO>> Login(IUserModel user)
        {
            IUserModelRO userRO;
            var loginUser = Task.Run(() => _auth.LoginUser(user));
            try { userRO = await loginUser; } catch(System.Exception e) {
                userRO = new IUserModelRO { error = e.Message };
            }
            return userRO;
        }
    }
}