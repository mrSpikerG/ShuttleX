using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IChatRepository : IGenericRepoitory<Chat>
    {
        void AddUserToChat(int chatId, int userId);
        void RemoveUserFromChat(int chatId, int userId);
        IEnumerable<Chat> GetChatsWithUsers(Expression<Func<Chat, bool>> filter = null,  string includeProperties = "");
    }
}
