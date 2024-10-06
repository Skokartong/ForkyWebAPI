using ForkyWebAPI.Models;
using ForkyWebAPI.Models.AccountDTOs;

namespace ForkyWebAPI.Services.IServices
{
    public interface IAccountService
    {
        Task AddAccountAsync(NewAccountDTO accountDTO);
        Task DeleteAccountAsync(int accountId);
        Task UpdateAccountAsync(int accountId, UpdateAccountDTO accountDTO);
        Task<List<ViewAccountDTO>> GetAllAccountsAsync();
        Task<ViewAccountDTO> GetAccountByIdAsync(int accountId);
        Task<string> LogInAsync(LoginDTO loginDTO);
    }
}
