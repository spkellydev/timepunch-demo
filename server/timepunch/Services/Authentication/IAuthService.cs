using System.Threading.Tasks;
using timepunch.Models;

namespace timepunch.Services.Authentication
{
    public interface IAuthService
    {
        IUserModelRO CreateUser(IUserModel user);
        Task<IUserModelRO> LoginUser(IUserModel user);
    }
}