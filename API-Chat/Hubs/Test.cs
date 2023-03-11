using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API_Chat.Hubs
{
    [Authorize]
    public class Test:Hub
    {
        
        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            Groups.AddToGroupAsync(Context.ConnectionId, name);

            return base.OnConnectedAsync(); 
        }
        public async Task SendMessage(string user, string message,string who)
        {


            await Clients.Group(user).SendAsync("ReceiveMessage", user,message);

            await Clients.Group(who).SendAsync("ReceiveMessage", user, message);

            


        }
    }
}
