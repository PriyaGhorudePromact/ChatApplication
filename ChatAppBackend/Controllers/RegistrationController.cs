using AutoMapper;
using ChatAppBackend.Dto;
using ChatAppBackend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace ChatAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MyDbContext _myDbContext;
        private readonly IMapper _mapper;
        public RegistrationController(MyDbContext myDbContext, IMapper mapper)
        {
            _myDbContext = myDbContext;
            _mapper = mapper;
        }


        [HttpPost()]
        public IActionResult RegisterUser(UserRegistration user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var emailExist = _myDbContext.User.Where(u => u.Email == user.Email).FirstOrDefault();

                if (emailExist != null)
                {
                    return Conflict("Email already exists");
                }
                var USER = _mapper.Map<User>(user);
                _myDbContext.User.Add(USER);
                _myDbContext.SaveChanges();
                var User = _mapper.Map<UserDto>(USER);
                return Ok(User);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing the registration request. " + ex.Message });
            }
        }
    }
}
