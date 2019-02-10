using timepunch.Models;

namespace timepunch.Services.Authentication
{
    public class AuthService : IAuthService
    {
        public IUserModelRO CreateUser(IUserModel user)
        {
            if (user.password.Length < 5)
            {
                throw new System.Exception("password is less than 6 characters");
            }
            var userResponseObj = new IUserModelRO {
                username = user.username,
                error = "ok"
            };
            return userResponseObj;
        }
    }
}