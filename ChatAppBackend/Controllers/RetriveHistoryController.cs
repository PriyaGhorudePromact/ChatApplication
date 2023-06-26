using ChatAppBackend.Model;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize]
        [HttpGet("{id}")]
        public List<Messages> GetRetriveHistory(string userId, DateTime before, int count, string sort)
        {
            var msg = _myDbContext.Messages.Where(x => x.senderId == userId).ToList();

            List<Messages> data = new List<Messages>();

            //RetriveData retriveData = new RetriveData();
            //if (msg != null)
            //{
            //    foreach (var item in msg)
            //    {
            //        RetriveData list = new RetriveData();
            //        list.userId = item.senderId;
            //        list.before = item.timestamp;
            //        list.count = item.content.Length;

            //        data.Add(list);
            //    }
            //}

            return msg;
        }
    }
}
