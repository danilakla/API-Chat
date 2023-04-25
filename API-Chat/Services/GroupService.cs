using API_Chat.Data;
using API_Chat.DTO;
using API_Chat.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Chat.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationContext applicationContext;

        public GroupService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AcceptInviteGrop(string roomName, string emailForConversationName, string ownEmail)
        {
            try
            {
                var conversation = await applicationContext.Conversations
                          .Where(e => e.ConversationName.Contains($"{roomName}{emailForConversationName}"))
                          .FirstOrDefaultAsync();
                var user = await applicationContext.Contacts.Include(e => e.Conversations).Where(e => e.Email == ownEmail).FirstOrDefaultAsync();
                user.Conversations.Add(conversation);
                await applicationContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task CreateGrop(CreateGropDto createGropDto)
        {
            try
            {
                var initiater = await applicationContext.Contacts
                    .Include(e => e.Conversations)
                    .Where(e => e.Email == createGropDto.FromWhom)
                    .FirstOrDefaultAsync();
                var conversation = new Conversations
                {

                    IsGroup = true,
                    RoomName = createGropDto.RoomName,
                    ConversationName = $"{createGropDto.RoomName}{initiater.Email}"
                };
                initiater.Conversations.Add(conversation);
                var notificationGr = new Notifications()
                {
                    FromWhom = createGropDto.FromWhom,
                    MessageText = createGropDto.Desription,
                    RoomName = createGropDto.RoomName,
                };
                await SendBroadcastNotification(createGropDto.SearchString, notificationGr);
                await applicationContext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<Conversations>> GetGroups()
        {
            try
            {
                var user = await applicationContext.Contacts.Include(e => e.Conversations).FirstOrDefaultAsync();
                var groups = user.Conversations.Where(e=>e.IsGroup=true).ToList();
                return groups;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task SendBroadcastNotification(string searchStr, Notifications notifications)
        {
            try
            {
                var contacts = await applicationContext.Contacts
                               .Include(e => e.Notifications)
                               .Where(e => e.BroadcastText.StartsWith(searchStr))
                               .ToListAsync();
                foreach (var contact in contacts)
                {
                    contact.Notifications.Add(notifications);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
