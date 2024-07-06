using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(DatabaseContext context) : base(context)
        {
        }

        public void AddUserToChat(int chatId, int userId)
        {
            var chat = Context.Chats.Include(c => c.Users).FirstOrDefault(c => c.Id == chatId);
            var user = Context.Users.FirstOrDefault(u => u.Id == userId);

            if (chat != null && user != null)
            {
                chat.Users.Add(user);
                Context.SaveChanges();
            }
        }

        public void RemoveUserFromChat(int chatId, int userId)
        {
            var chat = Context.Chats.Include(c => c.Users).FirstOrDefault(c => c.Id == chatId);
            var user = Context.Users.FirstOrDefault(u => u.Id == userId);

            if (chat != null && user != null)
            {
                chat.Users.Remove(user);
                Context.SaveChanges();
            }
        }
        public IEnumerable<Chat> GetChatsWithUsers(Expression<Func<Chat, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<Chat> query = Context.Chats;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();

        }
    }
}
