using System;
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
                userRO = new IUserModelRO { username = "", error = e.Message };
            }
            return userRO;
        }
    }
}