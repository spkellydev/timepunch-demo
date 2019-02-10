namespace timepunch.Models
{
    public class IUserModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class IUserModelRO
    {
        public string username { get; set; }
        public string error { get; set; }
    }
}