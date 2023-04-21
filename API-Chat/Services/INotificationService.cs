using API_Chat.DTO;
using API_Chat.Model;

namespace API_Chat.Services
{
	public interface INotificationService
	{
		Task<List<Notifications>>GetNotifications(string email);
		Task SendNotification(CreateNotificationDTO  createNotificationDTO);

		Task DeleteNotification(int Id);

	}
}
