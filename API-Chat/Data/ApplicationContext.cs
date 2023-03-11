using API_Chat.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Chat.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {


        }

        public DbSet<Contacts> Contacts{ get; set; }
        public DbSet<Conversations> Conversations { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Notifications> Notifications { get; set; }

    }

}
