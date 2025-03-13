using BankingSystem.Models;

namespace BankingSystem.Services;

public class AccountServices
{
    private readonly BankingsystemdbContext _context;
    private readonly ILogger<AccountServices> _logger;

    public AccountServices(BankingsystemdbContext context, ILogger<AccountServices> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Account CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
       _logger.LogInformation($"Account {account.Id} has been created");
        return account;
    }

    public Account ModifyAccount(Account account)
    {
        var existingAccount = _context.Accounts.Local.FirstOrDefault(a => a.Id == account.Id);
        if (existingAccount == null)
        {
            _logger.LogWarning($"Account {account.Id} does not exist");
            return null;
        }
        _context.Accounts.Update(account);
        _context.SaveChanges();
       _logger.LogInformation($"Account {account.Id} has been updated");
        return account;
    }
    
    
}

public class AccountDto
{
    public long Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }
}