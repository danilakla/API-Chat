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
	public class ChatController : ControllerBase
	{
		private readonly IChatService chatService;

		public ChatController(IChatService chatService)
		{
			this.chatService = chatService;
		}
		[HttpGet("/get-messages/{roomName}")]
		public async Task<List<Messages>> GetMessages(string roomName)
		{
			try
			{
				var messages=await chatService.GetMessages(roomName);
				return messages;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
