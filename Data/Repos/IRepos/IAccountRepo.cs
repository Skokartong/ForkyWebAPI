using ForkyWebAPI.Models;

namespace ForkyWebAPI.Data.Repos.IRepos
{
    public interface IAccountRepo
    {
        Task AddAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);
        Task UpdateAccountAsync(int accountId, Account updatedAccount);
        Task<List<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task<Account?> AccountExistsByEmailAsync(string email);
        Task<Account?> AccountExistsByUsernameAsync(string username);
    }
}
