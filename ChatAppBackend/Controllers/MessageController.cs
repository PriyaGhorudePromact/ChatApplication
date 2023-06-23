﻿using AutoMapper;
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
        public IActionResult SendMessage([FromBody]Messages message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var message1 = new Messages()
                {
                    senderId = message.senderId,
                    receiverId = message.receiverId,
                    content = message.content,
                    timestamp = message.timestamp
                };

                var msg = _mapper.Map<Messages>(message);
                _myDbContext.Messages.Add(msg);
                _myDbContext.SaveChanges();
                var msg1 = _mapper.Map<Messages>(msg);

                return Ok("Message Send By " + message.senderId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Message sending fail by " + message.senderId + ex.Message });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("EditMessage/{id}")]
        public IActionResult EditMessage(string id, [FromBody]Messages message)
        {
            var msg = _myDbContext.Messages.Where(x => x.Id == id).FirstOrDefault();
           // var msg =  _myDbContext.Messages.FindAsync(id);

            //add  if id consist in Db or not???
     
            if(msg != null && msg.content != message.content)
            {
                msg.senderId = message.senderId;
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

        [HttpDelete]
        [Route("DeleteMessage/{id}")]
        public IActionResult DeleteMessage(string id)
        {
            var msg = _myDbContext.Messages.Where(x => x.Id == id).FirstOrDefault();

            if(msg != null)
            {
                _myDbContext.Messages.Remove(msg);
                _myDbContext.SaveChanges();
                return Ok("Messatge Deleted");
            }
            else
            {
                return Ok("Fail to delete");
            }
           
        }

    }
}
