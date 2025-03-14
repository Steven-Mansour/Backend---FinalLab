using System.Text.Json.Serialization;
using BankingSystem.Models;

namespace BankingSystem.Services;

public class TransactionServices
{
    private readonly BankingsystemdbContext _context;
    private readonly ILogger<TransactionServices> _logger;

    public TransactionServices(BankingsystemdbContext context, ILogger<TransactionServices> logger)
    {
        _logger = logger;
        _context = context;
    }

    public List<Transaction> GetAllTransactions()
    {
        return _context.Transactions.ToList();
    }

    public Transaction CreateTransaction(Transaction transaction)
    {
        long? accountID = transaction.Accountid;
        if (_context.Accounts.Find(accountID) == null)
        {
            _logger.LogWarning($"Account {accountID} does not exist");
            return new Transaction();
        }
        _context.Transactions.Add(transaction);
        _context.SaveChanges();
        return transaction;
    }

    public List<TransactionDto> GetTransactionsByAccountId(long accountID)
    {
        return _context.Transactions
            .Where(t => t.Accountid == accountID)
            .Select(t => new TransactionDto
            {
                TransactionLogId = t.Id,
                TransactionType = t.Transactiontype,
                Amount = t.Amount,
                Timestamp = t.Timestamp,
                Status = t.Status
            })

            .ToList();
    }

    public List<CommonTransactionsDto> GetCommonTransactions(List<long> accountIds)
    {
        var transactions = _context.Transactions
            .Where(t => accountIds.Contains(t.Accountid.Value)) 
            .ToList();
        
        var commonAmountTransactions = transactions
            .GroupBy(t => t.Amount)
            .Where(g => g.Count() > 1)
            .Select(g => new CommonTransactionsDto
            {
                Amount = g.Key,
                TransactionType = null,
                AccountIds = g.Select(t => t.Accountid.Value).Distinct().ToList()
            });
        var commonTypeTransactions = transactions
            .GroupBy(t => t.Transactiontype)
            .Where(g => g.Count() > 1)
            .Select(g => new CommonTransactionsDto
            {
                Amount = null,
                TransactionType = g.Key,
                AccountIds = g.Select(t => t.Accountid.Value).Distinct().ToList()
            });
        return commonAmountTransactions.Concat(commonTypeTransactions).ToList();
    }
}
public class TransactionDto
{
    public long TransactionLogId { get; set; }
    public string? TransactionType { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? Timestamp { get; set; }
    public string? Status { get; set; }
}

public class CommonTransactionsDto
{
    public long TransactionLogId { get; set; }
    public List<long> AccountIds { get; set; }
    public string? TransactionType { get; set; }
    public decimal? Amount { get; set; }
}