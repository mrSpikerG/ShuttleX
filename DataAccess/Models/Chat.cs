using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string ChatName { get; set; }
        public int UserCreatorId { get; set; } 
        public User UserCreator { get; set; } = null!;
        public ICollection<Message> Messages { get; } = new List<Message>();
        public ICollection<User> Users{ get; } = new List<User>();

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TimeOfCreation { get; set; }

    }
}
