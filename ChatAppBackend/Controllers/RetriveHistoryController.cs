using ChatAppBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetriveHistoryController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;
        public RetriveHistoryController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public Messages AuthenticateUser(string id)
        {
            var User = _myDbContext.Messages.Where(x => x.Id == id).FirstOrDefault();
            return User;
        }

        [HttpGet("id")]
        public IActionResult GetRetriveHistory(string id)
        {
            var history = AuthenticateUser(id);
          // var data = _myDbContext.Messages.Where(x => x.Id == id).ToList();
          if(history != null)
            {
                return (IActionResult)_myDbContext.Messages.Where(x => x.Id == id).FirstOrDefault();
            }
   
            return Ok("Hi");
        }
    }
}
