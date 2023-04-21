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
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService notificationService;
		private readonly IdentityClaimsService identityClaimsService;

		public NotificationController(INotificationService notificationService, IdentityClaimsService identityClaimsService)
		{
			this.notificationService = notificationService;
			this.identityClaimsService = identityClaimsService;
		}
		[HttpGet("/get-notifications")]
		public async Task<List<Notifications>> GetNotification()
		{
			try
			{
				var claims = User.Identities.First();
				var email = identityClaimsService.GetUserEmail(claims);
				var notifications = await notificationService.GetNotifications(email);

				return notifications;
			}
			catch (Exception)
			{

				throw;
			}
		
		}

		[HttpPost("/send-notification")]
		public async Task SendNotification(CreateNotificationDTO createNotificationDTO)
		{
			try
			{
				var claims = User.Identities.First();
				var email = identityClaimsService.GetUserEmail(claims);
				createNotificationDTO.FromWhom = email;
				await notificationService.SendNotification(createNotificationDTO);

			}
			catch (Exception)
			{

				throw;
			}
	
		}

		[HttpDelete("/delete-notification/{id:int}")]
		public async Task SendNotification(int id)
		{
			try
			{

				await notificationService.DeleteNotification(id);

			}
			catch (Exception)
			{

				throw;
			}

		}
	}
}
