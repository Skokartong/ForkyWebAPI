using ForkyWebAPI.Models;

namespace ForkyWebAPI.Data.Repos.IRepos
{
    public interface IAccountRepo
    {
        // ADD, DELET, UPDATE ACCOUNTS
        Task AddAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
        Task UpdateAccountAsync(Account updatedAccount);

        // GET ACCOUNTS
        Task<List<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task<Account?> AccountExistsByEmailAsync(string email);
        Task<Account?> AccountExistsByUsernameAsync(string username);
    }
}
