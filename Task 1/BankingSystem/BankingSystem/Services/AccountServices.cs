using BankingSystem.Models;

namespace BankingSystem.Services;

public class AccountServices
{
    private readonly BankingsystemdbContext _context;

    public AccountServices(BankingsystemdbContext context)
    {
        _context = context;
    }

    public void CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public void ModifyAccount(Account account)
    {
        _context.Accounts.Update(account);
        _context.SaveChanges();
    }
    
    
}