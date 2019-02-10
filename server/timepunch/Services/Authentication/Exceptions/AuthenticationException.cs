using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using timepunch.Exceptions;

namespace timepunch.Services.Authentication.Exceptions
{
    [System.Serializable]
    public class AuthenticationException : TimepunchException
    {
        public AuthException code { get; set; }
        public AuthenticationException(AuthException ex) : base(ex) { }
        public AuthenticationException() { }
    }

    #region AuthException Enum
    /// <summary>
    /// Authentication based exceptions. When none is provided in the IUserModelRO, it will return as 0;
    /// Otherwise the TimepunchException base class will resolve the description
    /// </summary>
    public enum AuthException
    {
        [Description("ok")]
        OK,
        [Description("Could not create user with that username")]
        EXISTING_USER,
        [Description("Username should be between 3 and 16 characters")]
        USERNAME_LENGTH,
        [Description("Username shouldn't contain symbols")]
        USERNAME_SYMBOLS,
        [Description("Password should be between 6 and 15 characters")]
        PW_LENGTH,
        [Description("Password should have at least one number")]
        PW_SHOULD_CONTAIN_NUMBERS,
        [Description("Password should have at least one uppercase letter")]
        PW_SHOULD_HAVE_UPPERCASE,
        [Description("Password should have at least one lowercase letter")]
        PW_SHOULD_HAVE_LOWERCASE,
        [Description("Password should have at least one special character")]
        PW_SHOULD_HAVE_SYMBOLS,
        [Description("Internal server error")]
        USER_COULD_NOT_BE_CREATED,
    }
    #endregion
}
