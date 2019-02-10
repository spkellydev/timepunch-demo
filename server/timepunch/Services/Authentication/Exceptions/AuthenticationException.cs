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
        public AuthenticationException(AuthException ex) : base(ex) { }
    }

    #region AuthException Enum
    /// <summary>
    /// Authentication based exceptions
    /// </summary>
    public enum AuthException
    {
        [Description("Password should be at least 6 characters")]
        PASSWORD_LENGTH,
        [Description("")]
        NONE
    }
    #endregion
}
