using System;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Tools;
using ODT_Service.Interface;
using ODT_Model.DTO.Request;

namespace ODT_API.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions([FromQuery] QueryObject queryObject)
        {
            try
            {
                var transactions = await _transactionService.GetAllTransactionsAsync(queryObject);
                return CustomResult("Data loaded!", transactions);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetTransactionById/{id}")]
        public async Task<IActionResult> GetTransactionById(long id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);

            if (transaction == null)
            {
                return CustomResult("Transaction not found", HttpStatusCode.NotFound);
            }

            return CustomResult("Data loaded!", transaction);
        }

        [HttpGet("GetAllTransactionByWalletId/{walledId}")]
        public async Task<IActionResult> GetAllTransactionByWalletId(long walledId, [FromQuery] QueryObject queryObject)
        {
            try
            {
                var transactions = await _transactionService.GetAllTransactionByWalletIdAsync(walledId, queryObject);
                return CustomResult("Data loaded!", transactions);

            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest transactionRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                var createdTransaction = await _transactionService.CreateTransactionAsync(transactionRequest);
                return CustomResult("Created successfully", createdTransaction);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("UpdateTransaction/{transactionId}")]
        public async Task<IActionResult> UpdateTransaction(long transactionId, [FromBody] TransactionRequest transactionRequest)
        {
            if (!ModelState.IsValid)
            {
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            try
            {
                var updatedTransaction = await _transactionService.UpdateTransactionAsync(transactionRequest, transactionId);
                return CustomResult("Updated successfully", updatedTransaction);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("DeleteTransaction/{transactionId}")]
        public async Task<IActionResult> DeleteTransaction(long transactionId)
        {
            try
            {
                await _transactionService.DeleteTransactionAsync(transactionId);
                return CustomResult("Deleted successfully", HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetTotalRevenueFromDeposit")]
        public async Task<IActionResult> GetTotalRevenueFromDeposit()
        {
            try
            {
                var total = await _transactionService.GetTotalRevenue();
                return CustomResult("Data loaded!", total);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetTransactionsByDateRange")]
        public async Task<IActionResult> GetTransactionsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsByDateRange(startDate, endDate);
                return CustomResult("Data loaded!", transactions);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
