using ForkyWebAPI.Models;
using ForkyWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForkyWebAPI.Data.Repos.IRepos;

namespace ForkyWebAPI.Data.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly ForkyContext _context;

        public AccountRepo(ForkyContext context)
        {
            _context = context;
        }

        public async Task AddAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAccountAsync(int accountId, Account updatedAccount)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account != null)
            {
                account.FirstName = updatedAccount.FirstName;
                account.LastName = updatedAccount.LastName;
                account.Phone = updatedAccount.Phone;
                account.Address = updatedAccount.Address;
                account.Email = updatedAccount.Email;
                account.UserName = updatedAccount.UserName;
                account.PasswordHash = updatedAccount.PasswordHash; 
                account.Role = updatedAccount.Role;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == accountId);
        }

        public async Task<Account?> AccountExistsByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Account?> AccountExistsByUsernameAsync(string username)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == username);
        }
    }
}
