using API_Chat.Data;
using API_Chat.DTO;
using API_Chat.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Chat.Services
{
	public class NotificationService : INotificationService
	{
		private readonly ApplicationContext applicationContext;
		private readonly IFriendService friendService;

		public NotificationService(ApplicationContext applicationContext, IFriendService friendService)
		{
			this.applicationContext = applicationContext;
			this.friendService = friendService;
		}
		private Notifications PrepareForDatabase(CreateNotificationDTO createNotificationDTO)
		{
			return new() { FromWhom=createNotificationDTO.FromWhom,MessageText=createNotificationDTO.MessageBody, };
		}
		public async Task SendNotification(CreateNotificationDTO createNotificationDTO)
		{
			try
			{
				var user = await applicationContext.Contacts.Include(e => e.Notifications).Where(e=>e.Id==createNotificationDTO.FriendId).FirstOrDefaultAsync();
				var friends = await friendService.GetFriends(user.Email);
				if(friends is not null) { 
				}
			

				var Interfriends = await friendService.GetFriends(createNotificationDTO.FromWhom);
				if(Interfriends is not null)
				{
					foreach (var item in Interfriends)
					{
						if (item.Email.Equals(user.Email))
						{
							throw new Exception("had yeat initat");
						};
					}
				}
					
				var notificaiton = PrepareForDatabase(createNotificationDTO);
				user.Notifications.Add(notificaiton);
				await applicationContext.SaveChangesAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<List<Notifications>> GetNotifications(string email)
		{
			try
			{
				var user = await applicationContext.Contacts.Include(e=>e.Notifications).Where(e => e.Email.Equals(email)).FirstOrDefaultAsync();
				return user.Notifications;
			}
			catch (Exception)
			{

				throw;
			}	
		}

		public async Task DeleteNotification(int Id)
		{
			var notificationDelete = await applicationContext.Notifications.FindAsync(Id);
			 applicationContext.Notifications.Remove(notificationDelete);
				await applicationContext.SaveChangesAsync();
		}
	}
}
