using AutoMapper;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(QueryObject queryObject)
        {
            Expression<Func<Order, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(queryObject.Search))
            {
                filter = order => order.PaymentCode.Contains(queryObject.Search) ||
                                  order.Description.Contains(queryObject.Search) ||
                                  order.TransactionId.ToString().Contains(queryObject.Search);
            }

            var orders = _unitOfWork.OrderRepository.Get(
                filter: filter,
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize);

            if (orders == null || !orders.Any())
            {
                throw new CustomException.DataNotFoundException("The order list is empty!");
            }

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<OrderResponse> GetOrderByIdAsync(long id)
        {
            var order = _unitOfWork.OrderRepository.GetByID(id);
            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest)
        {
            if (_unitOfWork.TransactionRepository.GetByID(orderRequest.TransactionId) == null)
            {
                throw new CustomException.DataNotFoundException($"Transaction with ID: {orderRequest.TransactionId} not found!");
            }

            var order = _mapper.Map<Order>(orderRequest);
            _unitOfWork.OrderRepository.Insert(order);
            _unitOfWork.Save();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<OrderResponse> UpdateOrderAsync(OrderRequest orderRequest, long orderId)
        {
            var order = _unitOfWork.OrderRepository.GetByID(orderId);

            if (order == null)
            {
                throw new CustomException.DataNotFoundException($"Order with ID: {orderId} not found");
            }

            _mapper.Map(orderRequest, order);
            order.CreateDate = DateTime.Now;
            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.Save();

            return _mapper.Map<OrderResponse>(order);
        }
        public async Task<OrderResponse> UpdateOrderByTransAsync(OrderRequest orderRequest, long orderId)
        {
             var order = await _unitOfWork.OrderRepository.GetOrderByPaymentAsync(orderId);

            if (order == null)
            {
                throw new CustomException.DataNotFoundException($"Order with ID: {orderId} not found");
            }

            _mapper.Map(orderRequest, order);
            order.CreateDate = DateTime.Now;
            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.Save();

            return _mapper.Map<OrderResponse>(order);
        }
        public async Task<OrderResponse> GetOrderByTransactionCodeAsync(long id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderByPaymentAsync(id);
            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<bool> DeleteOrderAsync(long orderId)
        {
            var order = _unitOfWork.OrderRepository.GetByID(orderId);

            if (order == null)
            {
                throw new CustomException.DataNotFoundException($"Order with ID: {orderId} not found");
            }

            _unitOfWork.OrderRepository.Delete(order);
            _unitOfWork.Save();
            return true;
        }
    }
}