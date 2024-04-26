using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentTypeService : InterfaceService<PaymentType>
    {
        private readonly InterfaceRepository<PaymentType> _PaymentTypeRepository;

        public PaymentTypeService(InterfaceRepository<PaymentType> PaymentTypeRepository)
        {
            _PaymentTypeRepository = PaymentTypeRepository;
        }
        public async Task<List<PaymentType>> GetAll()
        {
            return await _PaymentTypeRepository.GetAll();

        }

        public async Task<PaymentType?> GetItemById(int id)
        {
            return await _PaymentTypeRepository.GetItemById(id);

        }

        public async Task<PaymentType?> CreateItem(PaymentType paymentType)
        {
            return await _PaymentTypeRepository.CreateItem(paymentType);

        }

        public async Task<PaymentType?> EditItem(int id, PaymentType paymentType)
        {
            return await _PaymentTypeRepository.EditItem(id, paymentType);

        }

        public async Task<PaymentType?> DeleteItem(int id)
        {
            return await _PaymentTypeRepository.DeleteItem(id);

        }


    }
}
