using API_Chat.Controllers;
using API_Chat.DTO;
using API_Chat.Infrastucture;
using API_Chat.Model;
using API_Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace API_Chat.Hubs
{
	[Authorize]
    public class Chat:Hub
    {
		private readonly IChatService chatService;
		private readonly IdentityClaimsService identityClaimsService;

		public Chat(IChatService chatService, IdentityClaimsService identityClaimsService)
        {
			this.chatService = chatService;
			this.identityClaimsService = identityClaimsService;
		}
		
        public async override Task OnConnectedAsync()
        {
			try
			{
			string email = identityClaimsService.GetUserEmail(Context.User.Identities.First());


			await Groups.AddToGroupAsync(Context.ConnectionId, email);
				await base.OnConnectedAsync();

				
			}
			catch (Exception)
			{

				throw;
			}
          
        }


		
		

		
		public async Task SendMessage(CreateMessageDTO createMessageDTO)
        {
			string FromWhom = identityClaimsService.GetUserEmail(Context.User.Identities.First());
			var message = new Messages {MessageText=createMessageDTO.MessageText,
            Time=DateTime.Now,FromWhom=FromWhom};
                await chatService.AddMessage(message, createMessageDTO.nameRoom);


                await Clients.Group(FromWhom).SendAsync("ReceiveMessage",message);

                await Clients.Group(createMessageDTO.ToWhom).SendAsync("ReceiveMessage", message);
            
           
         

            


        }
    }
}
