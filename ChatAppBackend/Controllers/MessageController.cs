using AutoMapper;
using ChatAppBackend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;
        private readonly IMapper _mapper;

        public MessageController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendMessage(Messages messages)
        {
            IActionResult response = Unauthorized();
            if (messages.content != null && messages.senderId != null)
            {
                _myDbContext.Messages.Add(messages);
            }

            return null;
        }
    }
}
