using System.ComponentModel.DataAnnotations;

namespace API_Chat.Model
{
    public class Contacts
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
		public string LastName{ get; set; } = string.Empty;
		public string Email{ get; set; } = string.Empty;
		public string BroadcastText { get; set; } = string.Empty;

		public byte [] Photo { get; set; }



        public List<Conversations> Conversations { get; set; } = new();
        
        public List<Notifications> Notifications{ get; set; }=new();

    }
}
