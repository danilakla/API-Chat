namespace API_Chat.DTO
{
	public class CreateMessageDTO
	{
		public string MessageText{ get; set; }
		public DateTime  TimeSendMessage { get; set; }
		public string  FromWhom{ get; set; }
		public string  ToWhom { get; set; }
		public string nameRoom { get; set; 
		
		}
	}
}
