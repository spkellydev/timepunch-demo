using timepunch.Models;
using timepunch.Services.Authentication.Exceptions;

namespace timepunch.Services.Authentication
{
    public class AuthService : IAuthService
    {
        public IUserModelRO CreateUser(IUserModel user)
        {
            // throw password length exception
            if (user.password.Length <= 5) throw new AuthenticationException(AuthException.PASSWORD_LENGTH);

            var userResponseObj = new IUserModelRO {
                username = user.username,
                error = AuthException.NONE
            };
            return userResponseObj;
        }
    }
}