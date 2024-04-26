using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : InterfaceService<User>
    {
        private readonly InterfaceRepository<User> _UserRepository;

        public UserService(InterfaceRepository<User> UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public async Task<List<User>> GetAll()
        {
            return await _UserRepository.GetAll();

        }

        public async Task<User?> GetItemById(int id)
        {
            return await _UserRepository.GetItemById(id);

        }

        public async Task<User?> CreateItem(User user)
        {
            return await _UserRepository.CreateItem(user);

        }

        public async Task<User?> EditItem(int id, User user)
        {
            return await _UserRepository.EditItem(id, user);

        }

        public async Task<User?> DeleteItem(int id)
        {
            return await _UserRepository.DeleteItem(id);

        }


    }
}
