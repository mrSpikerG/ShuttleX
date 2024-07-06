using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class UserService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public UserService(IMessageRepository messageRepository, IChatRepository chatRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }
        public User GetById(int id)
        {
            return _userRepository.FindById(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.Get();
        }

        public void CreateUser(User user)
        {
            _userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteUser(User user)
        {
            
            foreach(var item in _messageRepository.Get(item => item.UserCreatorId == user.Id))
            { 
                _messageRepository.Delete(item);    
            }
            foreach (var item in _chatRepository.Get(item => item.UserCreatorId == user.Id))
            {
                _chatRepository.Delete(item);
            }

            _userRepository.Delete(user);
        }
    }
}
