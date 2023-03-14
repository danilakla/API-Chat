using API_Chat.Data;
using API_Chat.DTO;
using API_Chat.DTO.Conversation;
using API_Chat.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace API_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly IDistributedCache distributedCache;

        public ConversationController(ApplicationContext db, IDistributedCache distributedCache )
        {
            this.db = db;
            this.distributedCache = distributedCache;
        }
        [HttpGet("loadFriend")]
        public async Task<ActionResult<List<CreateFriendDto>>> GetFriends(string user)
        {
            var vc=await db.Contacts.SelectMany(e=>e.Conversations).Include(e=>e.Contacts).Where(e=>e.ConversationName.Contains(user)).ToListAsync();
            List<CreateFriendDto> friendList = new();
            foreach (var item in vc)
            {
                foreach (var friend in item.Contacts)
                {
                    if (friend.Name != user)
                    {
                        friendList.Add(new CreateFriendDto { Name = friend.Name });
                    }
                }
            }


            return friendList.DistinctBy(e => e.Name).ToList();
        }
        [HttpPost("add-friend")]
        public async Task<IActionResult> AddFriend(CreateConversationDto createConversationDto)
        {

            try
            {
                var user =await db.Contacts.Include(e=>e.Conversations).FirstOrDefaultAsync(e => e.Name == createConversationDto.User);
                if (user is null) throw new Exception();

                var userFriend = await db.Contacts.Include(e => e.Conversations).FirstOrDefaultAsync(e => e.Name == createConversationDto.UserFriend);
                if (user is null) throw new Exception();

               var HasConversation= userFriend.Conversations.FirstOrDefault(e => e.ConversationName == $"{createConversationDto.UserFriend}$//${createConversationDto.User}");
                if (HasConversation is null)
                {
                    var nameConv = $"{createConversationDto.User}$//${createConversationDto.UserFriend}";
                    var conversation = new Conversations { ConversationName = nameConv, };
                    user.Conversations.Add(conversation);
                    await distributedCache.SetStringAsync(nameConv, JsonConvert.SerializeObject(new List<GetMessagesDto>()), default);

                }
                else
                {
                    user.Conversations.Add(HasConversation);

                }

                await db.SaveChangesAsync();
                return Ok(new { EmailOfFriend = createConversationDto.UserFriend });


            }
            catch (Exception)
            {

                return BadRequest();
            }


        }
    }
}
