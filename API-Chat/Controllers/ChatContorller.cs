using API_Chat.Data;
using API_Chat.DTO.Contact;
using API_Chat.DTO.Conversation;
using API_Chat.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace API_Chat.Controllers
{
    public class Frind
    {
        public string EmailOfFriend { get; set; }

    }
    [Route("api/[controller]")]
    [ApiController]
    public class ChatContorller : ControllerBase
    {


        private readonly IDistributedCache distributedCache;
        private readonly ApplicationContext db;

        public ChatContorller(IDistributedCache distributedCache,ApplicationContext db )
        {
            this.distributedCache = distributedCache;
            this.db = db;
        }


        [HttpPost("createProfile")]
        public async Task<IActionResult> CreateContact(CreateContactsDto createContactsDto)
        {
            try
            {
            db.Contacts.Add(new Contacts { Name = createContactsDto.Name });
            await db.SaveChangesAsync();
            return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }



    }
}
