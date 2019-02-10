using System;
using System.Text.RegularExpressions;
using timepunch.Models;
using timepunch.Services.Authentication.Exceptions;
using timepunch.Validation;

namespace timepunch.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly TimepunchContext _ctx;
        public AuthService(TimepunchContext ctx) { _ctx = ctx; }
        public IUserModelRO CreateUser(IUserModel user)
        {
            // check for existing user
            var existingUser = _ctx.Users.Find(user.username);
            if (existingUser != null) ThrowWithCode(AuthException.EXISTING_USER);

            // check for a valid username
            try { ValidateUsername(user.username); } catch(AuthenticationException ex) { throw ex; }
            // check for a valid password
            try { ValidatePassword(user.password); } catch(AuthenticationException ex) { throw ex; }
            user.password = HashPassword(user.password);

            // save user
            var createdUser = _ctx.Add(user);
            var saved = _ctx.SaveChanges();
            // saved should contain the number of rows affected
            if (saved < 1) ThrowWithCode(AuthException.USER_COULD_NOT_BE_CREATED);

            return new IUserModelRO {
                username = (string)createdUser.Property("username").CurrentValue,
                error = AuthException.OK
            };
        }

        #region Helpers
        /// <summary>
        /// Perform checks on the supplied username to ensure it meets:
        /// - Between 3 and 16 characters
        /// - No symbols
        /// </summary>
        /// <param name="username"></param>
        private void ValidateUsername(string username)
        {
            var len = username.Length;
            if (len <= 2 || len >= 15) ThrowWithCode(AuthException.USERNAME_LENGTH);
            if (StringValidator.HasSymbols(username)) ThrowWithCode(AuthException.USERNAME_SYMBOLS);
        }
        /// <summary>
        /// Perform checks on the supplied password to ensure it meets:
        /// - Between 6 and 16 characters
        /// - At least one capital letter
        /// - At least 2 numbers
        /// - At least 1 special character
        /// </summary>
        /// <param name="password"></param>
        private void ValidatePassword(string password)
        {
            var len = password.Length;
            if (len <= 5 || len >= 15) ThrowWithCode(AuthException.PW_LENGTH);
            if (!StringValidator.HasNumber(password)) ThrowWithCode(AuthException.PW_SHOULD_CONTAIN_NUMBERS);
            if (!StringValidator.HasUppercase(password)) ThrowWithCode(AuthException.PW_SHOULD_HAVE_UPPERCASE);
            if (!StringValidator.HasLowercase(password)) ThrowWithCode(AuthException.PW_SHOULD_HAVE_LOWERCASE);
            if (!StringValidator.HasSymbols(password)) ThrowWithCode(AuthException.PW_SHOULD_HAVE_SYMBOLS);
        }

        /// <summary>
        /// Throw an AuthenticationException with an AuthException code
        /// </summary>
        /// <param name="code">AuthException.CODE</param>
        /// <returns></returns>
        private AuthenticationException ThrowWithCode(AuthException code) => throw new AuthenticationException(code);

        /// <summary>
        /// Use Bcrypt to hash supplied password
        /// </summary>
        /// <param name="supplied">supplied password</param>
        /// <returns></returns>
        private string HashPassword(string supplied) => BCrypt.Net.BCrypt.HashPassword(supplied);
        #endregion
    }
}