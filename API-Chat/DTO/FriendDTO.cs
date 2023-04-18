using API_Chat.Model;

namespace API_Chat.DTO
{
	public class FriendDTO:Contacts
	{
		public int Id { get; set; }
		public string ConversationName { get; set; }
		public string Name { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public byte[] Photo { get; set; }

	}
}
