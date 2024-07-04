
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
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(QueryObject queryObject);
        Task<OrderResponse> GetOrderByIdAsync(long id);
        Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest);
        Task<OrderResponse> UpdateOrderAsync(OrderRequest orderRequest, long orderId);
        Task<bool> DeleteOrderAsync(long orderId);
        Task<OrderResponse> GetOrderByTransactionCodeAsync(long id);
        Task<OrderResponse> UpdateOrderByTransAsync(OrderRequest orderRequest, long orderId);
    }
}
