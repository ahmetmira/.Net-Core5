using GRTrkrar.Entities.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.Api.TokenModels
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //Token Üretecek Method
        public Token CreateToken()
        {
            Token token = new Token();

            //SecurityKey'in simetrik yansımasını alır
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            //Şifreli kimlik oluşturuluyor
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Token süresine 5 dk ekler
            token.Expiration = DateTime.Now.AddMinutes(5);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer:Configuration["Token:Issuer"],
                audience:Configuration["Token:Audience"],
                expires:token.Expiration,
                notBefore:DateTime.Now,         //Token üretildikten sonra süre ne zaman devreye girsin
                signingCredentials:signingCredentials
                );


            //Yeni bir acces token üretir
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            token.RefreshToken = CreateRefreshToken();
            return token;

        }

        public string CreateRefreshToken()
        {
            byte[] tokenArray = new byte[32];
            using(RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(tokenArray);

                return Convert.ToBase64String(tokenArray);
            }
        }
    }
}
