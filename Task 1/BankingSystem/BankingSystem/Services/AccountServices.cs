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

    public List<BalanceSummaryDto> GetBalanceSumamry(int userId)
    {
        var accounts = _context.Accounts
            .Where(a => a.Userid == userId)
            .ToList();
        decimal finalBalance = 0;
        var summaries = new List<BalanceSummaryDto>();
        foreach (var account in accounts)
        {
            var transactions = _context.Transactions
                .Where(t => t.Accountid == account.Id)
                .ToList();

            
            foreach (var transaction in transactions)
            {
             string type = transaction.Transactiontype;
             if (type == "Deposit")
             {
                 finalBalance += Convert.ToDecimal(transaction.Amount);
                
             } else if (type == "Withdraw")
             {
                 finalBalance -= Convert.ToDecimal(transaction.Amount);
             }
             BalanceSummaryDto summary = new BalanceSummaryDto();
             summary.AccountId = account.Id;
             summary.Amount = Convert.ToDecimal(transaction.Amount);
             summary.TransactionType = type;
             summaries.Add(summary);
            }
        }
        BalanceSummaryDto summaryDto = new BalanceSummaryDto
        {
            TransactionType = "Total Amount",
            Amount = finalBalance
        };
        summaries.Add(summaryDto);
        return summaries;
        
    }
    
    
}
public class BalanceSummaryDto
{
    public long? AccountId { get; set; }
    public string? TransactionType { get; set; } 
    public decimal? Amount { get; set; }
}

public class AccountDto
{
    public long Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }
}