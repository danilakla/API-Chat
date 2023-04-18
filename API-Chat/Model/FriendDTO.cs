namespace API_Chat.Model
{
    public class FriendDTO
    {
        public int Id { get; set; }
        public string ConversationName { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		public byte[] Photo { get; set; }

    }
}
