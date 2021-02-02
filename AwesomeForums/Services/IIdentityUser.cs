using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeForums.Services
{
    public interface IIdentityUser
    {
        IdentityUser GetById(string id);
        IEnumerable<IdentityUser> GetAll();
        
    }
}
