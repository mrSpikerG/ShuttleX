using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ChatDTO
    {

        public int Id { get; set; }
        public string ChatName { get; set; }

        public int UserCreatorId { get; set; }
        public List<MessageDTO> Messages { get; set; } 
        public List<UserDTO> Users { get; set; } 
        public DateTime TimeOfCreation { get; set; }
    }
}
