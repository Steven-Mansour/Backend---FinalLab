using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BankingSystem.Controllers;

[Route("api/[controller]")]
public class TransactionController: ControllerBase
{
    private readonly TransactionServices _transactionService;

    public TransactionController(TransactionServices transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    [Route("/transaction-logs/{accountId}")]
    public IActionResult GetTransactionLogs(int accountId)
    {
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