using API_Chat.Model;
using API_Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Chat.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]

	public class ContactController : ControllerBase
	{
		private readonly IContactService contactService;

		public ContactController(IContactService contactService)
		{
			this.contactService = contactService;
		}
		[HttpGet("/get-contacts")]
		public async Task<List<Contacts>> GetContacts(string name, string lastName)
		{
			try
			{
				var contacts = await contactService.GetContacts(new() { LastName = lastName, Name = name });
				return contacts;
			}
			catch (Exception)
			{

				throw;
			}
		
		}

	}
}
