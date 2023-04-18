using API_Chat.DTO;
using API_Chat.Model;

namespace API_Chat.Services
{
	public interface INotificationService
	{
		Task<List<Notifications>>GetNotifications(string email);
		void SendNotification(CreateNotificationDTO  createNotificationDTO);
	}
}
