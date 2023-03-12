using API_Chat.Controllers;
using API_Chat.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace API_Chat.Hubs
{
    [Authorize]
    public class Chat:Hub
    {
        private readonly IDistributedCache distributedCache;

        public Chat(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }
        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            Groups.AddToGroupAsync(Context.ConnectionId, name);

            return base.OnConnectedAsync(); 
        }
        public async Task SendMessage(string user, string message,string who)
        {

            CancellationToken cancellationToken = default;
            var findStr = $"{user}$//${who}";
            var res = await distributedCache.GetStringAsync(findStr);
            List<GetMessagesDto> messages = new();
            if (res is null)
            {
                findStr = $"{who}$//${user}";
                res = await distributedCache.GetStringAsync(findStr);
            

            }
          

            messages = JsonConvert.DeserializeObject<List<GetMessagesDto>>(res);

                messages.Add(new GetMessagesDto { From = user, Text = message });

                await distributedCache.SetStringAsync(findStr, JsonConvert.SerializeObject(messages), default);

                await Clients.Group(user).SendAsync("ReceiveMessage", user, message);

                await Clients.Group(who).SendAsync("ReceiveMessage", user, message);
            
           
         

            


        }
    }
}
