using System.ComponentModel.DataAnnotations;

namespace API_Chat.Model
{
    public class Conversations
    {
        [Key]
        public int Id { get; set; }
        public string ConversationName { get; set; } = string.Empty;

        public List<Contacts> Contacts { get; set; }
        public List<Messages> Messages { get; set; } 


    }
}
