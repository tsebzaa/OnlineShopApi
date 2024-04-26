using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderDetailService : InterfaceService<OrderDetail>
    {
        private readonly InterfaceRepository<OrderDetail> _OrderDetailRepository;

        public OrderDetailService(InterfaceRepository<OrderDetail> OrderDetailRepository)
        {
            _OrderDetailRepository = OrderDetailRepository;
        }
        public async Task<List<OrderDetail>> GetAll()
        {
            return await _OrderDetailRepository.GetAll();

        }

        public async Task<OrderDetail?> GetItemById(int id)
        {
            return await _OrderDetailRepository.GetItemById(id);

        }

        public async Task<OrderDetail?> CreateItem(OrderDetail orderDetail)
        {
            return await _OrderDetailRepository.CreateItem(orderDetail);

        }

        public async Task<OrderDetail?> EditItem(int id, OrderDetail orderDetail)
        {
            return await _OrderDetailRepository.EditItem(id, orderDetail);

        }

        public async Task<OrderDetail?> DeleteItem(int id)
        {
            return await _OrderDetailRepository.DeleteItem(id);

        }


    }
}
