using API_Chat.DTO;
using API_Chat.Model;

namespace API_Chat.Services
{
    public interface IGroupService
    {
        Task CreateGrop(CreateGropDto createGropDto);
        Task<List<Conversations>> GetGroups(string email);

        Task AcceptInviteGrop(string roomName, string emailConversationName, string ownEmail);

    }
}
