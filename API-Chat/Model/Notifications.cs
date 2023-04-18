
using System.Text.Json.Serialization;

namespace API_Chat.Model
{
    public class Notifications
    {
        public int Id { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public string FromWhom { get; set; } = string.Empty;

        public int ContactsId { get; set; }
        [JsonIgnore ]
        public Contacts Contacts { get; set; }


    }
}
