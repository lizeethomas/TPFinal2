using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPFinalStrasbourg.Repositories;
using TPFinalStrasbourg.Services;
using TPFinalStrasbourg.Models;
using TPFinalStrasbourg.DTOs;

namespace TPFinalStrasbourg.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : Controller
    {
        private JWTService _jwtService;
        private UserService _userService;

        public UserController(JWTService jwtService, UserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult DisplayAllUsers()
        {
            List<UserResponseDTO> responseDTOs = _userService.DisplayAll();
            if (responseDTOs != null)
            {
                return Ok(responseDTOs);
            }
            return BadRequest();
        }

        [HttpPost("newuser")]
        public IActionResult AddNewUser([FromBody] UserRequestDTO userRequestDTO)
        {
            UserResponseDTO userDTO = _userService.CreateUser(userRequestDTO);
            if (userDTO != null)
            {
                return Ok(userDTO);
            }
            return BadRequest();
        }

        [HttpPost("newadmin")]
        public IActionResult AddNewAdmin([FromBody] UserRequestDTO userRequestDTO )
        {
            UserResponseDTO userDTO = _userService.CreateAdmin(userRequestDTO);
            if (userDTO != null)
            {
                return Ok(userDTO);
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string email, [FromForm] string password)
        {
            string token = _jwtService.GetJWT(email, password);
            return Ok(token);
        }
    }
}
