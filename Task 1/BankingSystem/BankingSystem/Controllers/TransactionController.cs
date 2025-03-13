using BankingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[Route("api/[controller]")]
public class TransactionController: ControllerBase
{
    private readonly TransactionServices _transactionService;

    public TransactionController(TransactionServices transactionService)
    {
        _transactionService = transactionService;
    }
    
}