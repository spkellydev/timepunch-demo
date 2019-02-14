using System.Threading.Tasks;
using timepunch.Models;

namespace timepunch.Services
{
    public interface IAuthService
    {
        UserModelRO CreateUser(UserModel user);
        Task<UserModelRO> LoginUser(UserModel user);
    }
}