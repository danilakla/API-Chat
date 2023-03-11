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

        public ChatContorller(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }
        public List<Frind> frinds { get; set; } = new List<Frind>()
        {

            new Frind(){EmailOfFriend="Test"},
            new Frind(){EmailOfFriend="dany"},
                        new Frind(){EmailOfFriend="First"},

        };


        [HttpGet("test123")]


        public async Task<IActionResult> TestMy()

        {
            CancellationToken cancellationToken = default;
            await distributedCache.SetStringAsync("toti", JsonConvert.SerializeObject(new List<Frind> {       new Frind(){EmailOfFriend="Test"},
            new Frind(){EmailOfFriend="dany"},
                        new Frind(){EmailOfFriend="First"}}
            ), default);
            var res = await distributedCache.GetStringAsync("toti");
            var d = JsonConvert.DeserializeObject<List<Frind>>(res);

            return Ok(new { result = d});

        }
        [HttpPost("add-friend")]

        public async Task<IActionResult> AddFriend(Frind frind)
        {
            var newn = frinds.FirstOrDefault(e => e.EmailOfFriend.Contains(frind.EmailOfFriend));

            return Ok(newn);
        }


        [HttpGet("load-message")]
        public async Task<IActionResult> AddFriend(string conversationName)
        {

            return Ok();
        }
    }
}
