using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
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

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
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
    }
}
