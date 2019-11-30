using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace GAuth.Services
{
    public class TokenService
    {
        public static TokenContent GetTokenInfo(string token)
        {
            var client = new RestClient("https://www.googleapis.com");
            var request = new RestRequest("oauth2/v3/tokeninfo", Method.GET);
            request.AddParameter("id_token", token);

            var response = client.Execute<TokenContent>(request);

            // Create a ClaimsPrincipal and set Thread.CurrentPrincipal
            // var identity = new ClaimsIdentity();
            // identity.AddClaim(new Claim(ClaimTypes.Name, response.Data.Email));
            // Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

            return response.Data;
        }
    }

    public class TokenContent
    {
        public string Email { get; set; }
        public string EmailVerified { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Locale { get; set; }
        public string Exp { get; set; }
    }
}