using TPFinalStrasbourg.Models;
using TPFinalStrasbourg.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TPFinalStrasbourg.Services
{
    public class JWTService { 
    
        private UserRepository _userRepository;

        public JWTService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GetJWT(string email, string password)
        {
            User user = _userRepository.SearchOne(u => u.Email == email && u.Password == password);
            if (user != null && user.Status)
            {
                //Créer le token 
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Bonjour je suis la clé de sécurité pour générer la JWT")), SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, user.Role.RoleUser),
                        new Claim(ClaimTypes.Name, user.Name)
                    }),
                    Issuer = "LeMauvaisCoin",
                    Audience = "LeMauvaisCoin"

                };
                SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                return jwtSecurityTokenHandler.WriteToken(securityToken);
            }
            return null;
        }
    }
}
