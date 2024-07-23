
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interface
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponse>> GetAllTransactionsAsync(QueryObject queryObject);
        Task<TransactionResponse> GetTransactionByIdAsync(long id);
        Task<TransactionResponse> CreateTransactionAsync(TransactionRequest transactionRequest);
        Task<TransactionResponse> UpdateTransactionAsync(TransactionRequest transactionRequest, long transactionId);
        Task<bool> DeleteTransactionAsync(long transactionId);
        Task<IEnumerable<TransactionResponse>> GetTransactionsByDateRange(DateTime startDate, DateTime endDate);
        Task<TotalRevenueResponse> GetTotalRevenue();
        Task<IEnumerable<TransactionResponse>> GetAllTransactionByWalletIdAsync(long walletId, QueryObject queryObject);
    }
}
