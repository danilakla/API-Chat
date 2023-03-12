using System.ComponentModel.DataAnnotations;

namespace API_Chat.Model
{
    public class Contacts
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Contacts> FriendContact { get; set; } = new();

        public List<Conversations> Conversations { get; set; } = new();

        public List<Notifications> Notifications{ get; set; }

    }
}
