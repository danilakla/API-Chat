using API_Chat.Data;
using API_Chat.DTO;
using API_Chat.Model;

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
		public async void SendNotification(CreateNotificationDTO createNotificationDTO)
		{
			try
			{
				var user=await applicationContext.Contacts.FindAsync(createNotificationDTO.FriendId);
				var friends = await friendService.GetFriends(user.Email);
				foreach (var item in friends)
				{
					if (item.Email.Equals(createNotificationDTO.FromWhom)) {
						throw new Exception("had yeat");
					};
				}

				var Interfriends = await friendService.GetFriends(createNotificationDTO.FromWhom);
				foreach (var item in Interfriends)
				{
					if (item.Email.Equals(user.Email))
					{
						throw new Exception("had yeat initat");
					};
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
				var user=await applicationContext.Contacts.FindAsync(email);
				return user.Notifications;
			}
			catch (Exception)
			{

				throw;
			}	
		}
	}
}
