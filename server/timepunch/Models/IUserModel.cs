using System;
using System.ComponentModel.DataAnnotations;
using timepunch.Exceptions;

namespace timepunch.Models
{
    public class IUserModel
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
    }

    public class IUserModelRO
    {
        public string username { get; set; }
        public string token { get; set; }
        public object error { get; set; }
    }
}