using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Login { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public ICollection<Message> Messages { get; } = new List<Message>();
        public ICollection<Chat> Chats { get; } = new List<Chat>();
        public ICollection<Chat> ChatCreateds { get; } = new List<Chat>();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TimeOfCreation { get; set; }

    }
}
