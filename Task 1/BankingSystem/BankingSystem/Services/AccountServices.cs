using BankingSystem.Models;

namespace BankingSystem.Services;

public class AccountServices
{
    private readonly BankingsystemdbContext _context;

    public AccountServices(BankingsystemdbContext context)
    {
        _context = context;
    }

    public Account CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return account;
    }

    public Account ModifyAccount(Account account)
    {
        _context.Accounts.Update(account);
        _context.SaveChanges();
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