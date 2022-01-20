using GRTekrar.Api.Helpers;
using GRTekrar.Api.TokenModels;
using GRTekrar.DataAccess;
using GRTrkrar.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRTekrar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //readonly interface ve classları static yapmamızı sağlar
        private readonly GRTekrarDbContext context;
        private readonly IConfiguration configuration;
        private readonly GenericHelperMethods genericHelperMethods;

        public LoginController(GRTekrarDbContext context, IConfiguration configuration, GenericHelperMethods genericHelperMethods)
        {
            this.context = context;
            this.configuration = configuration;
            this.genericHelperMethods = genericHelperMethods;
        }

        //www.geldigitti.com/api/login/create  *controller==login* *action==create*
        [HttpPost("[action]")]
        public async Task<bool> Create([FromBody] User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return true;
        }

        [HttpPost("[action]")]
        public async Task<Token> Login([FromBody] UserLogin userlogin)
        {
            //password koumunu user tabloya eklemem lazım ve passwordHash ile değiştirmem gerekir
            User user = await context.Users.FirstOrDefaultAsync(w => w.Email == userlogin.Email && w.Password == userlogin.Password);
            if (user != null)
            {
                //GenericHelperMethods crearteToken = new GenericHelperMethods(context, configuration);
                //return await crearteToken.CreateRefreshToken(user);
                return await genericHelperMethods.CreateRefreshToken(user,context,configuration);
            }
            return null;
        }

        [HttpPost("[action]")]
        public async Task<Token> RefreshTokenLogin([FromForm] string refreshToken)
        {
            User user = await context.Users.FirstOrDefaultAsync(w => w.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.Now)
            {
                return await genericHelperMethods.CreateRefreshToken(user, context, configuration);
            }
            return null;
        }

        
    }
}
