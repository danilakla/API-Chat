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
    public class Chat:Hub
    {
		private readonly IChatService chatService;
		private readonly IdentityClaimsService identityClaimsService;

		public Chat(IChatService chatService, IdentityClaimsService identityClaimsService)
        {
			this.chatService = chatService;
			this.identityClaimsService = identityClaimsService;
		}
        public override Task OnConnectedAsync()
        {
            string email = identityClaimsService.GetUserEmail(Context.User.Identities.First());


			Groups.AddToGroupAsync(Context.ConnectionId, email);

            return base.OnConnectedAsync(); 
        }


		//public string MessageText { get; set; }
		//public DateTime TimeSendMessage { get; set; }
		//public string FromWhom { get; set; }
		//public string ToWhom { get; set; }
		//public string nameRoom
		//{
		//	get; set;
		//string MessageText, string FromWhom, string ToWhom, string nameRoom
		//var createMessageDTO = new CreateMessageDTO
		//{

		//	FromWhom = FromWhom,
		//	MessageText = MessageText,
		//	nameRoom = nameRoom,
		//	TimeSendMessage = DateTime.Now,
		//	ToWhom = ToWhom
		//};
		public class Test
		{
			public int A { get; set; }
			public string B { get; set; }
			public DateTime Temes { get; set; } = DateTime.Now;
		}

		public async Task TestOne(Test createMessageDTO)
		{
			Console.WriteLine("Dsd");

		}
		public async Task SendMessage(CreateMessageDTO createMessageDTO)
        {
            
			var message = new Messages {MessageText=createMessageDTO.MessageText,
            Time=createMessageDTO.TimeSendMessage};
                await chatService.AddMessage(message, createMessageDTO.nameRoom);


                await Clients.Group(createMessageDTO.FromWhom).SendAsync("ReceiveMessage",message);

                await Clients.Group(createMessageDTO.ToWhom).SendAsync("ReceiveMessage", message);
            
           
         

            


        }
    }
}
