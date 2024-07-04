using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(4000)]
        public string Content{ get; set; }
        public int UserCreatorId { get; set; }
        public User UserCreator { get; set; } = null!;
        public int ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TimeOfCreation { get; set; }
    }
}
