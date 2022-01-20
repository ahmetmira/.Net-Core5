using GRTekrar.Api.TokenModels;
using GRTekrar.DataAccess;
using GRTrkrar.Entities.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRTekrar.Api.Helpers
{
    public class GenericHelperMethods
    {
        //private readonly GRTekrarDbContext context;
        //private readonly IConfiguration configuration;

        //public GenericHelperMethods(GRTekrarDbContext context, IConfiguration configuration)
        //{
        //    this.context = context;
        //    this.configuration = configuration;
        //}

        public async Task<Token> CreateRefreshToken(User user, GRTekrarDbContext context, IConfiguration configuration)
        {
            //User için token üretiliyor
            TokenHandler tokenHandler = new TokenHandler(configuration);
            Token token = tokenHandler.CreateToken();

            //Refresh token kullanıcı tablosuna işleniyor
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenEndDate = token.Expiration.AddMinutes(5);
            await context.SaveChangesAsync();
            return token;
        }
    }
}
