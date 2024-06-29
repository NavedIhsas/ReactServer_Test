using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post(LoginDto login)
        {
            var result = new ResultDto();
            if (login is { Username: "admin", Password: "admin" })
            {
                result = new ResultDto()
                {
                    isSuccess = true,
                    Token="my token",
                    Data = new Data()
                    {
                        Family = "Ihsas",
                        Name = "Navid",
                        Token = "my token"
                    }
                };
            }

            return Ok(result);

        }

        [HttpGet]
        [Route("UserInfo")]
        public IActionResult UserInfo(string token)
        {
            var result = new ResultDto();
            if (token.Equals("my token"))
            {
                result = new ResultDto()
                {
                    isSuccess = true,
                    Token = "my token",
                    Data = new Data()
                    {
                        Family = "Ihsas",
                        Name = "Navid",
                        Token = "my token"
                    }
                };
            }
            return Ok(result);
        }

        public class LoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class ResultDto
        {
            public bool isSuccess { get; set; } = false;
            public string Token { get; set; }
            public Data Data { get; set; }
        }

        public class Data
        {
            public string Token { get; set; }
            public string Name { get; set; }
            public string Family { get; set; }
        }
    }
}
