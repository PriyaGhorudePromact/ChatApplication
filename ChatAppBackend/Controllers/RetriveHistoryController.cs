using ChatAppBackend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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


        [Authorize]
        [HttpGet]
        public IActionResult GetRetriveHistory(string userId, DateTime? before, int count = 20, string sort = "asc")
        {
            var MessageHistory = _myDbContext.Messages.Where(x => x.senderId == userId).ToList();

            if(MessageHistory != null)
            {
                var user = _myDbContext.Messages.Where(x => x.senderId == userId || x.receiverId == userId).FirstOrDefault();
                before = before != null ? before : DateTime.Now;
                if(sort == "desc")
                {
                    var sortedData = MessageHistory.OrderByDescending(s => s.timestamp).ToList();
                }
            }
            else
            {
                return Ok("User or conversation not found");
            }

            return Ok(MessageHistory);
        }
    }
}
