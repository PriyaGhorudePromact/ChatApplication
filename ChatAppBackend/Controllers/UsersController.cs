using ChatAppBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext userContext;

        public UsersController(MyDbContext userContext)
        {
            this.userContext = userContext;   
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<User> GetUsers()
        {
            return userContext.User.ToList();
        }

        [HttpGet]
        [Route("GetUser")]
        public User GetUser(int id)
        {
            return userContext.User.Where(x => x.Id == id).FirstOrDefault();
        }

        //[HttpPost]
        //[Route("AddUser")]
        //public string AddUser(User users)
        //{
        //    string response = string.Empty;
        //    userContext.User.Add(users);
        //    userContext.SaveChanges();
        //    return "User added";
        //}
    }
}
