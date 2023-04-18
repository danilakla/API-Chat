using API_Chat.Model;

namespace API_Chat.Services
{
	public interface IChatService
	{
		Task CreateRoom(string nameRoom);

		Task<List<Messages>> GetMessages(string nameRoom);

		Task AddMessage(Messages  message,string nameRoom);

	}
}
