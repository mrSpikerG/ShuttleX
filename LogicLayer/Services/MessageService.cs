using DataAccessLayer.DTOs;
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
        public List<MessageDTO> GetMessagesByChat(int userId, int chatId)
        {
            var user = _userRepository.FindById(userId);

            var chat = _chatRepository.GetChatsWithUsers((item) => item.Id == chatId, includeProperties: "Users").ToList();
            if (chat.Count == 0)
            {
                throw new Exception("Chat not Exists");
            }

            if (!chat[0].Users.Contains(user))
            {
                throw new Exception("This chat doesn't contain this user");
            }

            List<MessageDTO> messageDTOs = new List<MessageDTO>();
            var temp = _messageRepository.Get((item) => item.ChatId == chatId).ToList();
            foreach (var item in temp)
            {
                messageDTOs.Add(new MessageDTO()
                {
                    Id = item.Id,
                    Content = item.Content,
                    TimeOfCreation = item.TimeOfCreation,
                    ChatId = item.ChatId,
                    UserCreatorId = item.UserCreatorId
                });
            }
            return messageDTOs;

        }
    }
}
