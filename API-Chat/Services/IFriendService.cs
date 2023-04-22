using API_Chat.DTO;
using API_Chat.Model;

namespace API_Chat.Services
{
    public interface IFriendService
	{
		Task AddFriend(AcceptNoficationDTO acceptNoficationDTO);

		Task DeleteFriend(string roomName);

		Task<List<FriendDTO>> GetFriends(string email);
	}
}
