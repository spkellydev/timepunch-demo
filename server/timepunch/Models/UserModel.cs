using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using timepunch.Exceptions;

namespace timepunch.Models
{
    public class UserModel
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
    }

    public class UserModelRO
    {
        public string username { get; set; }
        public string token { get; set; }
        public object error { get; set; }
    }

    public class UserProfileModel
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("username")]
        public UserModel User { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeModel Employee { get; set; }
    }
}