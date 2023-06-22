using ChatAppBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ChatAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public string registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MyConnection").ToString());
			SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName, Password, Email, IsActive) VALUES('"+registration.UserName+"','"+registration.Password+ "','"+registration.Email+"','"+registration.IsActive+"' )", con);
			con.Open();
			int i = cmd.ExecuteNonQuery();
			con.Close();
			if(i > 0)
			{
				return "Data inserted";
			}
			else
			{
				return "Error";
			}

			return "";
        }
    }
}
