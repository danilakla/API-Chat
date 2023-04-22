using API_Chat.DTO;
using API_Chat.Infrastucture;
using API_Chat.Model;
using API_Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Chat.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class FriendController : ControllerBase
	{
		private readonly IFriendService friendService;
		private readonly IdentityClaimsService identityClaimsService;

		public FriendController(IFriendService friendService, IdentityClaimsService identityClaimsService)
		{
			this.friendService = friendService;
			this.identityClaimsService = identityClaimsService;
		}
		[HttpGet("/get-friends")]
		public async Task<List<FriendDTO>> GetFriends()
		{
			try
			{
				var claims = User.Identities.First();
				var email = identityClaimsService.GetUserEmail(claims);
				var friendDTOs = await friendService.GetFriends(email);

				return friendDTOs;
			}
			catch (Exception)
			{

				throw;
			}

		}

		[HttpPost("/add-friends")]
		public async Task SendNotification(AcceptNoficationDTO  acceptNoficationDTO)
		{
			try
			{
				var claims = User.Identities.First();
				var email = identityClaimsService.GetUserEmail(claims);
				await friendService.AddFriend(acceptNoficationDTO);

			}
			catch (Exception)
			{

				throw;
			}

		}

		[HttpDelete("/delete-friend/{roomName}")]
		public async Task DeleteFriend(string roomName)
		{
			try
			{
				await friendService.DeleteFriend(roomName);

			}
			catch (Exception)
			{

				throw;
			}

		}

	}
}
