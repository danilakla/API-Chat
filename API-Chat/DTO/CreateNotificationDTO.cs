namespace API_Chat.DTO
{
	public class CreateNotificationDTO
	{
		public int FriendId { get; set; }
		public string MessageBody { get; set; }
		public string FromWhom { get; set; }
	}
}
