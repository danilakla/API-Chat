using API_Chat.DTO;
using API_Chat.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace API_Chat.Services
{
	public class ChatService : IChatService
	{
		private readonly IDistributedCache distributedCache;

		public ChatService(IDistributedCache distributedCache)
		{
			this.distributedCache = distributedCache;
		}

		public async Task AddMessage(Messages message,string nameRoom)
		{
			var messagesStr = await distributedCache.GetStringAsync(nameRoom);
			var messages = JsonConvert.DeserializeObject<List<Messages>>(messagesStr);
			messages.Add(message);
			await distributedCache.SetStringAsync(nameRoom, JsonConvert.SerializeObject(messages), default);

		}

		public async Task CreateRoom(string nameRoom)
		{
			try
			{
				await distributedCache.SetStringAsync(nameRoom, JsonConvert.SerializeObject(new List<Messages>()), default);

			}
			catch (Exception)
			{

				throw;
			}

		}

		public async Task<List<Messages>> GetMessages(string nameRoom)
		{
			try
			{
				CancellationToken cancellationToken = default;
				var messagesStr = await distributedCache.GetStringAsync(nameRoom);
				var messages = JsonConvert.DeserializeObject<List<Messages>>(messagesStr);

				return messages;
			}
			catch (Exception)
			{

				throw;
			}
		

		}
	}
}
