using System.Text.Json.Serialization;
using BankingSystem.Models;

namespace BankingSystem.Services;

public class TransactionServices
{
    private readonly BankingsystemdbContext _context;

    public TransactionServices(BankingsystemdbContext context)
    {
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
}
public class TransactionDto
{
    public long TransactionLogId { get; set; }
    public string? TransactionType { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? Timestamp { get; set; }
    public string? Status { get; set; }
}