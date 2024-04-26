using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : InterfaceService<Order>
    {
        private readonly InterfaceRepository<Order> _OrderRepository;

        public OrderService(InterfaceRepository<Order> OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }
        public async Task<List<Order>> GetAll()
        {
            return await _OrderRepository.GetAll();

        }

        public async Task<Order?> GetItemById(int id)
        {
            return await _OrderRepository.GetItemById(id);

        }

        public async Task<Order?> CreateItem(Order order)
        {
            return await _OrderRepository.CreateItem(order);

        }

        public async Task<Order?> EditItem(int id, Order order)
        {
            return await _OrderRepository.EditItem(id, order);

        }

        public async Task<Order?> DeleteItem(int id)
        {
            return await _OrderRepository.DeleteItem(id);

        }


    }
}
