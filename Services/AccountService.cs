using ForkyWebAPI.Models.AccountDTOs;
using ForkyWebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using ForkyWebAPI.Data.Repos.IRepos;
using ForkyWebAPI.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Org.BouncyCastle.Crypto.Generators;

namespace ForkyWebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IConfiguration _configuration;

        public AccountService(IAccountRepo accountRepo, IConfiguration configuration)
        {
            _accountRepo = accountRepo;
            _configuration = configuration;
        }

        public async Task AddAccountAsync(NewAccountDTO accountDTO)
        {
            var existingEmail = await _accountRepo.AccountExistsByEmailAsync(accountDTO.Email);
            if (existingEmail != null)
            {
                throw new Exception("Email is already taken");
            }

            var existingUser = await _accountRepo.AccountExistsByUsernameAsync(accountDTO.UserName);
            if (existingUser != null)
            {
                throw new Exception("Username is already taken");
            }

            if (accountDTO.Password != accountDTO.ConfirmPassword)
            {
                throw new Exception("Passwords do not match");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password);

            var account = new Account
            {
                UserName = accountDTO.UserName,
                FirstName = accountDTO.FirstName,
                LastName = accountDTO.LastName,
                Email = accountDTO.Email,
                Phone = accountDTO.Phone,
                Address = accountDTO.Address,
                PasswordHash = passwordHash,
                Role = accountDTO.UserName == "admin" ? "Admin" : "User"
            };

            await _accountRepo.AddAccountAsync(account);
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            await _accountRepo.DeleteAccountAsync(accountId);
        }

        public async Task UpdateAccountAsync(int accountId, UpdateAccountDTO accountDTO)
        {
            var existingAccount = await _accountRepo.GetAccountByIdAsync(accountId);

            if (existingAccount != null)
            {
                existingAccount.UserName = accountDTO.UserName;
                existingAccount.Email = accountDTO.Email;
                existingAccount.Address = accountDTO.Address;
                existingAccount.Phone = accountDTO.Phone;
                if (!string.IsNullOrEmpty(accountDTO.Password))
                {
                    existingAccount.PasswordHash = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password);
                }

                await _accountRepo.UpdateAccountAsync(accountId, existingAccount);
            }
        }

        public async Task<List<ViewAccountDTO>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepo.GetAllAccountsAsync();

            return accounts.Select(account => new ViewAccountDTO
            {
                UserName = account.UserName,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Phone = account.Phone,
                Address = account.Address,
                Email = account.Email
            }).ToList();
        }

        public async Task<ViewAccountDTO?> GetAccountByIdAsync(int accountId)
        {
            var account = await _accountRepo.GetAccountByIdAsync(accountId);

            if (account == null)
            {
                return null; 
            }

            return new ViewAccountDTO
            {
                UserName = account.UserName,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Phone = account.Phone,
                Address = account.Address,
                Email = account.Email
            };
        }

        public async Task<string> LogInAsync(LoginDTO loginDTO)
        {
            var existingUser = await _accountRepo.AccountExistsByUsernameAsync(loginDTO.UserName);

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, existingUser.PasswordHash))
            {
                throw new Exception("Invalid username or password");
            }

            return GenerateJwtToken(existingUser);
        }

        private string GenerateJwtToken(Account existingUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["JwtKey"];
            var issuer = _configuration["JwtIssuer"];
            var audience = _configuration["JwtAudience"];

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new Exception("JWT configuration values are missing.");
            }

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{existingUser.FirstName} {existingUser.LastName}"),
                new Claim(ClaimTypes.Email, existingUser.Email),
                new Claim(ClaimTypes.Role, existingUser.Role)
            };

            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

