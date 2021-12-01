using FP.ViewModels.Account;
using System.Threading.Tasks;

namespace FP.Interfaces.Account
{
    public interface IAccountService
    {
        Task<LoginResponseViewModel> LoginAsync(LoginRequestViewModel credentials);
        Task<RegisterResponseViewModel> RegisterAsync(RegisterRequestViewModel registrationDetails);
    }
}
