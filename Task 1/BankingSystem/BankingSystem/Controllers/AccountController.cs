using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;
[Route("/api/[controller]")]
public class AccountController:ControllerBase
{
    private readonly AccountServices _accountService;

    public AccountController(AccountServices accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [Route("/create-account")]
    public IActionResult CreateAccount([FromBody] AccountDto accountDto)
    {
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
    
}