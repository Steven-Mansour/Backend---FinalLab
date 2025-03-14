using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BankingSystem.Controllers;

[Route("api/[controller]")]
public class TransactionController: ControllerBase
{
    private readonly TransactionServices _transactionService;
    private readonly ILogger<TransactionController> _logger;
    public TransactionController(TransactionServices transactionService, ILogger<TransactionController> logger)
    {
        _transactionService = transactionService;
        _logger = logger;
    }

    [HttpGet]
    [Route("/transaction-logs/{accountId}")]
    public IActionResult GetTransactionLogs(int accountId)
    {
        _logger.LogInformation("Getting transaction logs for account {AccountId}", accountId);
        List<TransactionDto> list;
        list = _transactionService.GetTransactionsByAccountId(accountId);
        return Ok(list);
    }

    [EnableQuery]
    [HttpGet]
    [Route("/transaction-logs")]
    public IActionResult GetTransactionLogs([FromQuery] string filter,
        [FromQuery] string orderby, [FromQuery] int skip,[FromQuery] int top )
    {
        _logger.LogInformation("Getting all transaction logs");
        return Ok(_transactionService.GetAllTransactions());
    }

    [HttpPost]
    [Route("/transaction-logs")]
    public IActionResult CreateTransaction([FromBody] Transaction transaction)
    {
        
        _transactionService.CreateTransaction(transaction);
       
        return Ok(transaction );
    }

    
    
}