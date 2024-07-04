using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class ChatService
    {
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }
        public Chat GetById(int id)
        {
            return _chatRepository.FindById(id);
        }

        public IEnumerable<Chat> GetAll()
        {
            return _chatRepository.Get();
        }

        public void Create(Chat chat)
        {
            _chatRepository.Insert(chat);
        }

        public void Update(Chat chat)
        {
            _chatRepository.Update(chat);
        }

        public void Delete(Chat chat)
        {
            _chatRepository.Delete(chat);
        }

        public void CreateChat(int userId, string chatName)
        {
            var user = _userRepository.FindById(userId);
            var newChat = new Chat() { 
                ChatName = chatName, 
                UserCreator = user, 
                UserCreatorId = userId
            };
            newChat.Users.Add(user);
            
            _chatRepository.Insert(newChat);
        }
        public List<Chat> GetChatsByName(int userId, string chatName)
        {
            return _chatRepository.Get();
        }

    }
}
