using System.Threading.Tasks;
using timepunch.Models;

namespace timepunch.Services.Authentication
{
    public interface IAuthService
    {
        UserModelRO CreateUser(UserModel user);
        Task<UserModelRO> LoginUser(UserModel user);
    }
}