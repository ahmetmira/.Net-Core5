using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRTekrar.Api.Controllers
{
    public class IdNameResponse
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of entity
        /// </summary>
        public string Name { get; set; }
    }
}
