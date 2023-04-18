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
				List<Contacts> contacts=new();
				if (string.IsNullOrEmpty(findContactsDTO.LastName) && string.IsNullOrEmpty(findContactsDTO.Name))
				{
					contacts = await applicationContext.Contacts.Take(3).ToListAsync();
				}
				if ((!string.IsNullOrEmpty(findContactsDTO.LastName)) && string.IsNullOrEmpty(findContactsDTO.Name))
				{
					contacts = await applicationContext.Contacts
					 .Where(e => e.LastName.ToLower().Contains(findContactsDTO.LastName.ToLower()))
					 .ToListAsync();


				}
				if ((!string.IsNullOrEmpty(findContactsDTO.Name)) && string.IsNullOrEmpty(findContactsDTO.LastName))
				{
					contacts = await applicationContext.Contacts
				 .Where(e => e.Name.ToLower().Contains(findContactsDTO.Name.ToLower()))
				 .ToListAsync();
				}
				if((!string.IsNullOrEmpty(findContactsDTO.LastName)) && (!string.IsNullOrEmpty(findContactsDTO.Name)))
				{
					contacts = await applicationContext.Contacts
				   .Where(e =>
				   e.Name.ToLower().Contains(findContactsDTO.Name.ToLower())
				   &&
				   e.LastName.ToLower().Contains(findContactsDTO.LastName.ToLower()))
				   .ToListAsync();
				}
			

				return contacts;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
