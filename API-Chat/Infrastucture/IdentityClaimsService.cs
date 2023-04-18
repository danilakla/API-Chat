using System.Security.Claims;

namespace API_Chat.Infrastucture
{
public class IdentityClaimsService
	{
  

    public string GetUserEmail(ClaimsIdentity claimsIdentity)
    {
        try
        {

            var reqString = claimsIdentity.Claims.Where(c => c.Type == "Email").FirstOrDefault();

            return reqString.Value;
        }
        catch (Exception e)
        {

            throw e;
        }
    }
}

}
