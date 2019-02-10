using System;
using timepunch.Services.Authentication.Exceptions;

namespace timepunch.Models
{
    public class IUserModel
    {
        public Guid Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class IUserModelRO
    {
        public string username { get; set; }
        public object error { get; set; }
    }
}