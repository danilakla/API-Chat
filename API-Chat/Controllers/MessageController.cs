using API_Chat.Data;
using API_Chat.DTO;
using API_Chat.DTO.Conversation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace API_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationContext applicationContext;
        private readonly IDistributedCache distributedCache;

        public MessageController(ApplicationContext applicationContext, IDistributedCache distributedCache)
        {
            this.applicationContext = applicationContext;
            this.distributedCache = distributedCache;
        }
        [HttpGet("load-message")]
        public async Task<ActionResult<List<GetMessagesDto>>> GetMessages(string user, string userFriend)
        {

            CancellationToken cancellationToken = default;
            var findStr = $"{user}$//${userFriend}";
            var res = await distributedCache.GetStringAsync(findStr) ;
            List<GetMessagesDto> messages = new();
            if (res is null)
            {
                findStr = $"{userFriend}$//${user}";
                res = await distributedCache.GetStringAsync(findStr);
               
              
            }
            messages = JsonConvert.DeserializeObject<List<GetMessagesDto>>(res);


            return messages;
        }
    }
}
