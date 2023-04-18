using EventBus.Events;

namespace API_Chat.IntegrationEvents.Events
{
	public record CreateProfileBaseOnUniverDataIntegrationEvent: IntegrationEvent
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Photo { get; set; }
		public string BroadcastMessage { get; set; }
	}
}
