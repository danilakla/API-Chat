using API_Chat.Data;
using API_Chat.DTO;
using API_Chat.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Chat.Services
{
	public class ContactService : IContactService
	{
		private readonly ApplicationContext applicationContext;

		public ContactService(ApplicationContext applicationContext)
		{
			this.applicationContext = applicationContext;
		}
	
		public async Task<List<Contacts>> GetContacts(FindContactsDTO findContactsDTO)
		{
			try
			{
				List<Contacts> contacts = await applicationContext.Contacts
					   .Where(e =>
					   e.Name.ToLower().Contains(findContactsDTO.Name.ToLower())
					   &&
					   e.LastName.ToLower().Contains(findContactsDTO.LastName.ToLower()))
					   .ToListAsync();

				return contacts;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
