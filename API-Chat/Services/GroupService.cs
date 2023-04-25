using API_Chat.Data;
using API_Chat.DTO;
using API_Chat.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Chat.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IChatService chatService;

        public GroupService(ApplicationContext applicationContext, IChatService chatService)
        {
            this.applicationContext = applicationContext;
            this.chatService = chatService;
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
                await DeleteGropAccept(user.Id);
                await applicationContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }
        private async Task DeleteGropAccept(int id)
        {
            try
            {
                var notificationDelete = await applicationContext.Notifications.Where(e=>e.ContactsId== id).FirstOrDefaultAsync();
                applicationContext.Notifications.Remove(notificationDelete);
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
                await chatService.CreateRoom(conversation.ConversationName);
                await SendBroadcastNotification(createGropDto.SearchString, notificationGr, initiater.Email);
                await applicationContext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<Conversations>> GetGroups(string email)
        {
            try
            {
                var user = await applicationContext.Contacts
                    .Include(e => e.Conversations)
                    .Where(e=>e.Email==email)
                    .FirstOrDefaultAsync();
                var groups = user.Conversations.Where(e=>e.IsGroup==true).ToList();
                return groups;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task SendBroadcastNotification(string searchStr, Notifications createGropDto,string email)
        {
            try
            {
                var contacts = await applicationContext.Contacts
                               .Include(e => e.Notifications)
                               .Where(e => e.BroadcastText.StartsWith(searchStr))
                               .ToListAsync();
                foreach (var contact in contacts)
                {
                    if (contact.Email == email) continue;
                    var notification = new Notifications
                    {
                        FromWhom = createGropDto.FromWhom,
                        MessageText = createGropDto.MessageText,
                        RoomName = createGropDto.RoomName,
                    };
                    contact.Notifications.Add(notification);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
