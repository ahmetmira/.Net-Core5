using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRTrkrar.Entities.Model
{
    public class UserRole : IdentityRole<int>
    {
        public string Nmae { get; set; }
    }
}
