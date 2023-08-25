using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class UserMessage
    {
        [Key]
        public int Id { get; set; }
        public int SenderId { get; set; }
        public AppUser Sender { get; set; } // Navigation property

        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; } // ID of the user receiving the message
        public string Content { get; set; }
    }
}
