using DataAccessLayer.DTOs;
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
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }
       

        public void CreateChat(int userId, string chatName)
        {
            var user = _userRepository.FindById(userId);
            var newChat = new Chat()
            {
                ChatName = chatName,
                UserCreator = user,
                UserCreatorId = userId
            };
            newChat.Users.Add(user);
            user.Chats.Add(newChat);
            _chatRepository.Insert(newChat);
        }
        public List<ChatDTO> GetAvailableChatsByName(int userId, string chatName)
        {
            var user = _userRepository.FindById(userId);


         
            var availableChats = _chatRepository.GetChatsWithUsers((item) => item.ChatName.Contains(chatName) && !item.Users.Contains(user), includeProperties: "Users");
          

            return ChatToDTO(availableChats.ToList());

        }

        public List<ChatDTO> GetUserChatsByName(int userId, string chatName)
        {
            var user = _userRepository.FindById(userId);
            var userChats = _chatRepository.GetChatsWithUsers((item) => item.ChatName.Contains(chatName) && item.Users.Contains(user), includeProperties: "Users");
            
            return ChatToDTO(userChats.ToList());
        }

        public void ConnectToChat(int userId, int chatId)
        {
            _chatRepository.AddUserToChat(chatId, userId);
        }
        public void LeaveFromChat(int userId, int chatId)
        {
            _chatRepository.RemoveUserFromChat(chatId, userId);
        }
        public void DeleteChat(int userId, int chatId)
        {

            var chat = this._chatRepository.Get((item) => item.UserCreatorId == userId).ToList().Where(x => x.Id == chatId).FirstOrDefault();

            if (chat == null)
            {
                throw new Exception("This user does not have access to this chat");
            }
            _chatRepository.Delete(chat);

        }

        private List<ChatDTO> ChatToDTO(List<Chat> chats)
        {
            List<ChatDTO> chatDTOs = new List<ChatDTO>();
            foreach (var availableChat in chats)
            {

                List<UserDTO> users = new List<UserDTO>();
                foreach (var chatUsers in availableChat.Users)
                {
                    users.Add(new UserDTO()
                    {
                        Id = chatUsers.Id,
                        Age = chatUsers.Age,
                        Login = chatUsers.Login,
                        Name = chatUsers.Name,
                        Surname = chatUsers.Surname
                    });
                }

                List<MessageDTO> messages = new List<MessageDTO>();
                foreach (var message in availableChat.Messages)
                {
                    messages.Add(new MessageDTO()
                    {
                        ChatId = message.ChatId,
                        Content = message.Content,
                        Id = message.Id,
                        TimeOfCreation = message.TimeOfCreation,
                        UserCreatorId = message.UserCreatorId
                    });
                }


                chatDTOs.Add(new ChatDTO()
                {
                    ChatName = availableChat.ChatName,
                    TimeOfCreation = availableChat.TimeOfCreation,
                    Id = availableChat.Id,
                    UserCreatorId = availableChat.UserCreatorId,
                    Users = users,
                    Messages = messages
                });
            }
            return chatDTOs;
        }
    }
}
