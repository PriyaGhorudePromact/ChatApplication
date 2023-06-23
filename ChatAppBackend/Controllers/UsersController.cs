using ChatAppBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;

        public UsersController(MyDbContext _myDbContext)
        {
            this._myDbContext = _myDbContext;   
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<User> GetUsers()
        {
            return _myDbContext.User.ToList();
        }

        [HttpGet]
        [Route("GetUser")]
        public Messages GetUser(string id)
        {
            return _myDbContext.Messages.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
