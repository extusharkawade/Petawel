using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using Petawel.Controllers.Models;

namespace Petawel
{
    public class JwtAuthenticationManager
    {

        private readonly string key;
      //  private readonly IDictionary<string, string> users = new Dictionary<string, string>();
        //   {{"test","password"},{"test1","pwd"}};



     //   private readonly IConfiguration _configuration;

     
        public JwtAuthenticationManager(string key)
        {
        //    DbConnections dbConnection = new DbConnections();
      //   users=   dbConnection.Credentials();
       //     users.Add("test", "passworddd");
        
            this.key = key;
        }
        public string Authenticate(string username, string password, IDictionary<string, string> users)
        {
            if (!users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }
            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
                
                };
            var token = TokenHandler.CreateToken(tokenDescriptor);

            return TokenHandler.WriteToken(token);
        }

}
}
