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

        public MessageController(MyDbContext myDbContext, IMapper mapper)
        {
            _myDbContext = myDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetMessage")]
        public async Task<Messages> GetMessage(string id)
        {
            return _myDbContext.Messages.Where(x => x.Id == id).FirstOrDefault();
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendMessage(Messages message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = _myDbContext.Messages.Where(x => x.senderId == message.Id).FirstOrDefault();

                if(data == null)
                {
                    var msg = _mapper.Map<Messages>(message);
                    _myDbContext.Messages.Add(msg);
                    _myDbContext.SaveChanges();

                    return Ok("Message Send By " + message.senderId);
                }
                //else
                //{
                //    if (data.content != message.content)
                //    {
                //        //data.senderId = message.Id;
                //        data.receiverId = message.receiverId;
                //        data.content = message.content;
                //        data.timestamp = message.timestamp;
                //        data.senderId = message.senderId;

                //        _myDbContext.Update(data);
                //        _myDbContext.SaveChanges();

                //        return Ok("Message Updated Successfully By " + message.senderId);
                //    }
                //}
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Message sending fail by " + message.senderId + ex.Message });
            }
            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("EditMessage")]
        public IActionResult EditMessage(string id, Messages message)
        {
            var msg = _myDbContext.Messages.Where(x => x.senderId == id).FirstOrDefault();
     
            if(msg != null && msg.content != message.content)
            {
               // msg.senderId = message.Id;
                msg.receiverId = message.receiverId;
                msg.content  = message.content;
                msg.timestamp = message.timestamp;
                msg.senderId = message.senderId;

                _myDbContext.Update(msg);
                _myDbContext.SaveChanges();

                return Ok("Message Updated Successfully By " + message.senderId);
            }
            else
            {
                return Ok("Message Updated Failed " + message.senderId);
            }

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteMessage/{id}")]
        public IActionResult DeleteMessage(string id)
        {
            var msg = _myDbContext.Messages.Where(x => x.senderId == id).FirstOrDefault();

            if(msg !=null)
            {
                _myDbContext.Messages.Remove(msg);
                _myDbContext.SaveChanges();
                return Ok("Message Deleted By " + id);
            }
            else
            {
                return StatusCode(401, new { error = "You are not allowed to delete the message. "});
            }    
            return Ok();
        }

    }
}
