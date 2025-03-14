using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;
[Route("/api/[controller]")]
public class AccountController:ControllerBase
{
    private readonly AccountServices _accountService;
    private readonly TransactionServices _transactionService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(AccountServices accountService,
        ILogger<AccountController> logger, TransactionServices transactionService)
    {
        _logger = logger;
        _accountService = accountService;
        _transactionService = transactionService;
    }

    [HttpPost]
    [Route("/create-account")]
    public IActionResult CreateAccount([FromBody] AccountDto accountDto)
    {
        _logger.LogInformation("Creating account for {Email}", accountDto.Email);
        Account account = new Account
        {
            Id = accountDto.Id,
            Firstname = accountDto.Firstname,
            Lastname = accountDto.Lastname,
            Email = accountDto.Email
        };
       Account newAcc = _accountService.CreateAccount(account);
       
       return Ok(newAcc);
    }
    
    [HttpPost]
    [Route("/modify-account")]
    public IActionResult ModifyAccount([FromBody] AccountDto accountDto)
    {
        _logger.LogInformation("Modifying account for {Email}", accountDto.Email);
        Account account = new Account
            {
                Id = accountDto.Id,
                Firstname = accountDto.Firstname,
                Lastname = accountDto.Lastname,
                Email = accountDto.Email
            };
        Account newAcc = _accountService.ModifyAccount(account);
        
        return Ok(newAcc);
    }
    [HttpGet]
    [Route("/accounts/common-transactions")]
    public IActionResult GetCommonTransactions([FromQuery] List<long> accountIds)
    {
        if (accountIds == null || accountIds.Count == 0)
        {
            return BadRequest("Account IDs are required.");
        }
        var commonTransactions = _transactionService.GetCommonTransactions(accountIds);
        return Ok(commonTransactions);
    }

    [HttpGet]
    [Route("/accounts/balance-summary/{userId}")]
    public IActionResult GetAccountBalanceSummary([FromRoute] int userId)
    {
        List<BalanceSummaryDto> list = _accountService.GetBalanceSumamry(userId);
        return Ok(list);
    }
    
}