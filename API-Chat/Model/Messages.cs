using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Chat.Model
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MessageText { get; set; } = string.Empty;

        public DateTime Time { get; set; }

        public string FromWhom { get; set; }
        public int ConversationsId { get; set; }
        [JsonIgnore]

        public Conversations Conversations { get; set; }
    }
}
