using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using timepunch.Models;
using timepunch.Validation;
using timepunch.Exceptions;
using JWT.Builder;
using JWT.Algorithms;

namespace timepunch.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly TimepunchContext _ctx;
        public AuthService(TimepunchContext ctx) { _ctx = ctx; }
        public UserModelRO CreateUser(UserModel user)
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
            // return with token
            return new UserModelRO {
                username = (string)createdUser.Property("username").CurrentValue,
                token = AssignTokenToUser(createdUser.Entity),
                error = AuthException.OK
            };
        }

        async public Task<UserModelRO> LoginUser(UserModel user)
        {
            // see if user is in the db
            var foundUser = await _ctx.Users.FindAsync(user.username);
            if (foundUser == null) ThrowWithCode(AuthException.NO_EXISTING_USER);
            // check the password
            if (!ComparePassword(user.password, foundUser.password)) ThrowWithCode(AuthException.PW_DOESNT_MATCH);
            // return with token
            return new UserModelRO {
                username = (string)foundUser.username,
                token = AssignTokenToUser(foundUser),
                error = AuthException.OK
            };
        }

        #region Helpers
        /// <summary>
        /// Perform checks on the supplied username to ensure it meets:
        /// - Between 3 and 16 characters
        /// - No symbols
        /// </summary>
        /// <param name="username">unregistered username to validate</param>
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
        /// <param name="password">unhashed password to validate</param>
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

        private bool ComparePassword(string supplied, string hashed) => BCrypt.Net.BCrypt.Verify(supplied, hashed);

        /// <summary>
        /// After a user is authenicated, a token needs to be assigned to the user so they can access protected content
        /// </summary>
        /// <param name="authenticatedUser">An authenticated user should be passed, otherwise it's just chaos</param>
        /// <returns>string, token: { expiration, issuedAt, username }</returns>
        private string AssignTokenToUser(UserModel authenticatedUser) {
            var now = DateTimeOffset.UtcNow;
            var token = new JwtBuilder()
                            .WithAlgorithm(new HMACSHA256Algorithm())
                            .WithSecret("secret")
                            .AddClaim("exp", now.AddHours(1).ToUnixTimeMilliseconds())
                            .AddClaim("iat", now.ToUnixTimeMilliseconds())
                            .AddClaim("username", authenticatedUser.username)
                            .Build();
            return token;
        }
        #endregion
    }
}