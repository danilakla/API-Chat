using API_Chat.Data;
using API_Chat.IntegrationEvents.Events;
using API_Chat.Model;
using EventBus.Abstructions;
using System.Text;

namespace API_Chat.IntegrationEvents.EventHandling
{
	public class CreateContactsIntegrationEventHandler : IIntegrationEventHandler<CreateProfileBaseOnUniverDataIntegrationEvent>
	{
		private readonly ApplicationContext applicationContext;

		public CreateContactsIntegrationEventHandler(ApplicationContext applicationContext)
		{
			this.applicationContext = applicationContext;
		}
		public async Task Handle(CreateProfileBaseOnUniverDataIntegrationEvent @event)
		{
			var user = new Contacts { BroadcastText = @event.BroadcastMessage, 
				Email = @event.Email,
				LastName = @event.LastName,
				Name = @event.Name, 
				Photo = Encoding.UTF8.GetBytes(@event.Photo) };

			 applicationContext.Contacts.Add(user);
			await applicationContext.SaveChangesAsync();
			Console.WriteLine("test");
		}
	}
}
