using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Net.payOS;
using Net.payOS.Types;
using ODT_Model.DTO.Request;
using ODT_Service.Interface;

namespace ODT_API.Controllers.Transaction;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    // GET
    private readonly PayOS _payOS;
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;
    private readonly ITransactionService _transactionService;
    private readonly IWalletService _walletService;
    public PaymentController(PayOS payOS,
        IOrderService orderService,
        ITransactionService transactionService, 
        IWalletService walletService,
        IUserService userService
        )
    {
        _payOS = payOS;
        _orderService = orderService;
        _transactionService = transactionService;
        this._walletService = walletService;
        _userService = userService;
    }
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreatePaymentLink(CreatePaymentLinkRequest body)
    {
        try
        {
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            ItemData item = new ItemData(body.productName, 1, body.price);
            List<ItemData> items = new List<ItemData>();
            items.Add(item);
            PaymentData paymentData = new PaymentData(orderCode, body.price, body.description, items, body.cancelUrl, body.returnUrl);

            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
            OrderRequest orderRequest = new OrderRequest();
            TransactionRequest transactionRequest = new TransactionRequest();
            var userId = _userService.GetUserID();
            var walletId = await _walletService.GetWalletByUserIdAsync(long.Parse(userId));
            transactionRequest.WalletId = walletId.Id;
            transactionRequest.Description = paymentData.description;
            transactionRequest.Ammount = paymentData.amount;
            transactionRequest.Type = "Deposit";
            var trans = await _transactionService.CreateTransactionAsync(transactionRequest);
            orderRequest.TransactionId = trans.Id;
            orderRequest.PaymentCode = paymentData.orderCode.ToString();
            orderRequest.Description = paymentData.description;
            orderRequest.Money = paymentData.amount;
            orderRequest.Status = false;
            
            await _orderService.CreateOrderAsync(orderRequest);

            return Ok(new Response(0, "success", createPayment));
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception);
            return Ok(new Response(-1, "fail", null));
        }
    }
    [Authorize]
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder([FromRoute] int orderId)
    {
        try
        {
            PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(orderId);
            if (paymentLinkInformation.status.Equals("PAID"))
            {
                var order1 = await _orderService.GetOrderByTransactionCodeAsync(orderId);
                var trans1 = await _transactionService.GetTransactionByIdAsync(order1.TransactionId);
                
                OrderRequest orderRequest = new OrderRequest();
                orderRequest.Description = order1.Description;
                orderRequest.TransactionId = trans1.Id;
                orderRequest.Money = order1.Money;
                orderRequest.PaymentCode = orderId.ToString();
                orderRequest.Status = true;
                await _orderService.UpdateOrderByTransAsync(orderRequest, orderId);
                var order = await _orderService.GetOrderByTransactionCodeAsync(orderId);
                var trans = await _transactionService.GetTransactionByIdAsync(order.TransactionId);
                var wallet = await _walletService.GetWalletByIdAsync(trans.WalletId);
                WalletRequest walletRequest = new WalletRequest();
                walletRequest.Balance = wallet.Balance + paymentLinkInformation.amount;
                walletRequest.UserId = wallet.UserId;
                walletRequest.Status = true;
                await _walletService.UpdateWalletAsync(walletRequest, trans.WalletId);


            }
            return Ok(new Response(0, "Ok", paymentLinkInformation));
        }
        catch (System.Exception exception)
        {

            Console.WriteLine(exception);
            return Ok(new Response(-1, "fail", null));
        }

    }
    [HttpPut("{orderId}")]
    public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
    {
        try
        {
            PaymentLinkInformation paymentLinkInformation = await _payOS.cancelPaymentLink(orderId);
            return Ok(new Response(0, "Ok", paymentLinkInformation));
        }
        catch (System.Exception exception)
        {

            Console.WriteLine(exception);
            return Ok(new Response(-1, "fail", null));
        }

    }
    [HttpPost("confirm-webhook")]
    public async Task<IActionResult> ConfirmWebhook(ConfirmWebhook1 body)
    {
        try
        {
            await _payOS.confirmWebhook(body.webhook_url);
            return Ok(new Response(0, "Ok", null));
        }
        catch (System.Exception exception)
        {

            Console.WriteLine(exception);
            return Ok(new Response(-1, "fail", null));
        }

    }
    public record Response(
        int error,
        String message,
        object? data
    );
    public record ConfirmWebhook1(
        string webhook_url
    );
}