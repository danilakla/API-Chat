using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Chat.Model
{
    public class Conversations
    {
        [Key]
        public int Id { get; set; }
        public string ConversationName { get; set; } = string.Empty;
		public string RoomName{ get; set; } = string.Empty;

		public bool IsGroup { get; set; }
        [JsonIgnore]
        public List<Contacts> Contacts { get; set; }
        public List<Messages> Messages { get; set; } 


    }
}
