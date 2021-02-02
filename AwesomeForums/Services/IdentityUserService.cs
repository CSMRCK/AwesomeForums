//using AwesomeForums.Data;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AwesomeForums.Services
//{
//    public class IdentityUserService : IIdentityUser
//    {
//        private readonly ApplicationDbContext _context;

//        public IdentityUserService(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IEnumerable<IdentityUser> GetAll()
//        {
//            return _context.IdentityUsers;
//        }

//        public IdentityUser GetById(string id)
//        {
//           //return GetAll().FirstOrDefault(user = > user.Id == id);
//        }
//    }
//}
