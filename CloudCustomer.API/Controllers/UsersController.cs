using CloudCustomer.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsers();

            if(users is null || !users.Any())
                return NotFound();
            return Ok(users);
        }
    }
}
