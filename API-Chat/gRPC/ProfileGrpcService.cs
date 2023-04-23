using API_Chat.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ProfileService.Proto;

namespace API_Chat.gRPC
{
	public class ProfileGrpcService:Profile.ProfileBase
	{
		private readonly ApplicationContext applicationContext;

		public ProfileGrpcService(ApplicationContext applicationContext)
		{
			this.applicationContext = applicationContext;
		}
	
		public async  override Task<Empty> UpdateIcon(Icon request, ServerCallContext context)
		{
			try
			{
				var user = await applicationContext.Contacts.Where(e => e.Email.Equals(request.Email)).FirstOrDefaultAsync();
				if (user is not null)
				{
					user.Photo = request.Photo.ToArray();
					await applicationContext.SaveChangesAsync();
				}
				return new() { };
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
