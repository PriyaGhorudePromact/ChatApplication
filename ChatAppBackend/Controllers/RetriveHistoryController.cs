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
        [HttpGet("{Userid}")]
        public IActionResult GetRetriveHistory(string userId, DateTime before, int count, string sort = "asc")
        {
            var MessageHistory = _myDbContext.Messages.Where(x => x.senderId == userId).ToList();
            List<Messages> ResultMessageHistory = new List<Messages>();

            int maxCount = 20;
            if (count > maxCount)
            {
                return Ok("Max user can fetch 20 messages from conversatiuon history.");
            }
            if (MessageHistory != null)
            {
                var user = _myDbContext.Messages.Where(x => x.senderId == userId || x.receiverId == userId).FirstOrDefault();
               
                before = before != null ? before : DateTime.Now;
                foreach (var message in MessageHistory)
                {
                    bool isBeforeCurrentTimestamp = message.timestamp < before;
                    if (isBeforeCurrentTimestamp)
                    {
                        ResultMessageHistory.Add(message);
                    }
                }
                
                if (sort == "desc")
                {
                    var sortedData = MessageHistory.OrderByDescending(s => s.timestamp).ToList();
                }
            }
            else
            {
                return Ok("User or conversation not found");
            }
            var result = ResultMessageHistory.OrderBy(i => i.timestamp).Take(count);
            return Ok(result);
        }
    }
}
