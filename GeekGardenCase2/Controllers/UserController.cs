
using GeekGardenCase2.Models;
using GeekGardenCase2.Models.Request;
using GeekGardenCase2.Models.Response;
using GeekGardenCase2.Services;
using GeekGardenCase2.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekGardenCase2.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IJwtUtil jwtUtil;

        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, IJwtUtil jwt, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.jwtUtil = jwt;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var curUser = (User)HttpContext.Items["User"];
                if (curUser?.Role.Id == 1) //admin
                {
                    var users = await userService.GetAll();
                    return Ok(users);
                }
                else if (curUser?.Role.Id == 2 && curUser.Department !=null) //manager
                {
                    var users = await userService.GetAllByDepartment(curUser.Id);
                    return Ok(users);
                }
                else if (curUser?.Role.Id == 3) //employee
                {
                    var user = await userService.GetById(curUser.Id);
                    return Ok(user);
                }
                else
                {
                    return BadRequest(new
                    {
                        message = "Bad Request"
                    });
                }
            } catch(Exception ex)
            {
                logger.LogError(ex, "Error getting users");
                return Problem("Something is wrong");
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest userReq)
        {
            var user = await userService.Authenticate(userReq.Username, userReq.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var token = jwtUtil.GenerateToken(user);

            return Ok(new AuthResponse(user,token));

        }
    }
}
