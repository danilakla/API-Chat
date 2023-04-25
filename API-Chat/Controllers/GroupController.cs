using API_Chat.DTO;
using API_Chat.Infrastucture;
using API_Chat.Model;
using API_Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace API_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IdentityClaimsService identityClaimsService;

        public GroupController(IGroupService groupService, IdentityClaimsService identityClaimsService) 
        {
            this.groupService = groupService;
            this.identityClaimsService = identityClaimsService;
        }
        [HttpPost("/create-group")]

        public async Task<IActionResult> CreateGroup(CreateGropDto createGropDto)
        {
            try
            {
                var claims = User.Identities.First();
                var email = identityClaimsService.GetUserEmail(claims);
                createGropDto.FromWhom = email;
                await groupService.CreateGrop(createGropDto);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("/get-groups")]

        public async Task<List<Conversations>> GetGroups()
        {
            try
            {
                var claims = User.Identities.First();
                var email = identityClaimsService.GetUserEmail(claims);
               var groups= await groupService.GetGroups(email);
                return groups;

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("/acceptInvete-group")]

        public async Task<IActionResult> AcceptInvite(string roomName, string emailForRoom)
        {
            try
            {

                var claims = User.Identities.First();
                var email = identityClaimsService.GetUserEmail(claims);
                await groupService.AcceptInviteGrop(roomName, emailForRoom, email);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
