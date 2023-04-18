using System.ComponentModel.DataAnnotations;

namespace API_Chat.Model
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string message_text { get; set; } = string.Empty;
        [Required]
        public string FromWhom { get; set; }= string.Empty;
        public DateTime Time { get; set; }

		public int ConversationsId { get; set; }

        public Conversations Conversations { get; set; }
    }
}
