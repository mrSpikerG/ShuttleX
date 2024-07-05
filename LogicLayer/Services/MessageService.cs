using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class MessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public MessageService(IMessageRepository messageRepository, IChatRepository chatRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }
        public Message GetById(int id)
        {
            return _messageRepository.FindById(id);
        }

        public IEnumerable<Message> GetAll()
        {
            return _messageRepository.Get();
        }

        public void Create(Message message)
        {
            _messageRepository.Insert(message);
        }

        public void Update(Message message)
        {
            _messageRepository.Update(message);
        }

        public void Delete(Message message)
        {
            _messageRepository.Delete(message);
        }
        public void CreateMessage(int userId, int chatId, string content)
        {

            var user = _userRepository.FindById(userId);
            var chat = _chatRepository.FindById(chatId);

            if (chat == null)
            {
                throw new Exception("Chat not Exists");
            }

            var newMessage = new Message()
            {
                Content = content,
                ChatId = chatId,
                Chat = chat,
                UserCreator = user,
                UserCreatorId = userId
            };

            _messageRepository.Insert(newMessage);

        }
        public List<Message> GetMessagesByChat(int userId, int chatId)
        {
            var user = _userRepository.FindById(userId);
            var chat = _chatRepository.FindById(chatId);

            if (chat == null)
            {
                throw new Exception("Chat not Exists");
            }

            if (!chat.Users.Contains(user))
            {
                throw new Exception("This chat doesn't contain this user");
            }

            return _messageRepository.Get((item) => item.ChatId == chatId).ToList();
            
        }
    }
}
