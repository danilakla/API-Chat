using API_Chat.DTO;
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
		public async Task CreateRoom(string nameRoom)
		{
			try
			{
				await distributedCache.SetStringAsync(nameRoom, JsonConvert.SerializeObject(new List<GetMessagesDto>()), default);

			}
			catch (Exception)
			{

				throw;
			}

		}
	}
}
