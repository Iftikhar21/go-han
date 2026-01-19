using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using go_han.DTOs.Auth;
using go_han.DTOs.User;
using go_han.Interface;
using go_han.Mappers;
using go_han.Repositories.IRepository;
using go_han.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace go_han.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;

        public AuthController(
                IAuthRepository repository,
                IUserRepository userRepository,
                IJwtUtils jwtUtils
            )
        {
            this._repository = repository;
            this._userRepository = userRepository;
            this._jwtUtils = jwtUtils;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserReqDto userReqDto)
        {
            var user = await _repository.Register(userReqDto.ToUserReqDto());
            if (user == null)
                return Conflict(ResponseResult.Fail<UserDto>("User already exist"));

            var getUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (getUser == null)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            return Ok(ResponseResult.Success(getUser.ToUserDto(), "Register successful"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _repository.Login(userLoginDto.Email, userLoginDto.Password);
            if (user == null)
                return NotFound(ResponseResult.Fail<UserDto>("Invalid email or password"));

            var token = _jwtUtils.GenerateJwtToken(user);
            if (token == null)
                return NotFound(ResponseResult.Fail<UserDto>("Token not found"));

            var response = new AuthDto
            {
                Token = token
            };

            return Ok(ResponseResult.Success(response, "Login successful"));
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound(ResponseResult.Fail<UserDto>("User not found or token not valid"));

            var user = await _userRepository.GetUserByIdAsync(int.Parse(userId.Value));
            if (user == null)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            return Ok(ResponseResult.Success(user.ToUserDto(), "User found"));
        }
    }
}