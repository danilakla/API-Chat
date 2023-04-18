using API_Chat.DTO;
using API_Chat.Model;

namespace API_Chat.Services
{
    public interface IContactService
    {
        Task<List<Contacts>> GetContacts(FindContactsDTO findContactsDTO);
    }
}
