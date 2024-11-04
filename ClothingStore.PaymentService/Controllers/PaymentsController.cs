using ClothingStore.PaymentService.Data;
using ClothingStore.PaymentService.DTOs;
using ClothingStore.PaymentService.Interfaces;
using ClothingStore.PaymentService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.PaymentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly PaymentServiceDbContext _context;
    private readonly IPaymentService _paymentService;

    public PaymentsController(PaymentServiceDbContext context, IPaymentService paymentService)
    {
        _context = context;
        _paymentService = paymentService;
    }

    [HttpPost("payment-process")]
    public async Task<ActionResult<Payment>> ProcessPayment([FromBody] PaymentRequest request)
    {
        // Process payment with Stripe, using the token from the client
        var paymentIntent = await _paymentService.CreatePaymentIntentAsync(
            request.Amount, request.Currency);

        if (paymentIntent == null)
        {
            return BadRequest("Payment processing failed.");
        }

        // Store the transaction details in the database
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Amount = request.Amount,
            Currency = request.Currency,
            PaymentDate = DateTime.UtcNow,
            Status = PaymentStatus.Success,
            PaymentProvider = "Stripe",
            TransactionId = paymentIntent.Id
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return Ok(payment);
    }

    // Retrieve payment history
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentHistory(Guid userId)
    {
        var payments = await _context.Payments
            .Where(p => p.UserId == userId)
            .ToListAsync();

        return Ok(payments);
    }
}
