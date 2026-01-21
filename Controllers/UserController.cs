using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.User;
using go_han.Mappers;
using go_han.Models;
using go_han.Repositories.IRepository;
using go_han.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace go_han.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _repository.GetUsersAsync();
            if (!users.Any())
                return Ok(ResponseResult.Success("No users found"));

            var usersDto = users.Select(x => x.ToUserDto()).ToList();
            return Ok(ResponseResult.Success(usersDto, "Users found"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            return Ok(ResponseResult.Success(user.ToUserDto(), "User found"));
        }
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _repository.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            return Ok(ResponseResult.Success(user.ToUserDto(), "User found"));
        }
        [HttpPost]
        public async Task<IActionResult> CreatedUser([FromBody] UserReqDto userReqDto)
        {
            var listUser = userReqDto.ToUserReqDto();
            var user = await _repository.CreatedUserAsync(listUser);
            if (user == null)
                return Conflict(ResponseResult.Fail<UserDto>("User already exist"));

            var getUser = await _repository.GetUserByIdAsync(user.Id);
            if (getUser == null)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = user.Id },
                ResponseResult.Success(getUser.ToUserDto(), "User created")
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserReqDto userReqDto)
        {
            var listUser = userReqDto.ToUserReqDto();
            var existUser = await _repository.GetUserByIdAsync(id);
            if (existUser == null)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            var user = await _repository.UpdateUserAsync(id, listUser);
            if (user == null)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            return Ok(ResponseResult.Success(user.ToUserDto(), "User updated"));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repository.DeleteUserAsync(id);
            if (user == false)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            return Ok(ResponseResult.Success(user, "User deleted"));
        }

        [HttpGet("{id}/role")]
        public async Task<IActionResult> GetRoleUser(int id)
        {
            var users = await _repository.GetUsersByRoleAsync(id);
            if (!users.Any())
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            var userDto = users.Select(x => x?.ToUserDto()).ToList();
            return Ok(ResponseResult.Success(userDto, "User found"));
        }

        [HttpGet("employee/role")]
        public async Task<IActionResult> GetUsersEmployeeAsync()
        {
            var users = await _repository.GetUsersEmployeeAsync();
            if (!users.Any())
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            var userDto = users.Select(x => x?.ToUserDto()).ToList();
            return Ok(ResponseResult.Success(userDto, "User found"));
        }

        [HttpPatch("{id}/role")]
        public async Task<IActionResult> UpdateRoleUser(int id, int roleId)
        {
            var user = await _repository.UpdateRoleUserAsync(id, roleId);
            if (user == false)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            var getUser = await _repository.GetUserByIdAsync(id);
            if (getUser == null)
                return NotFound(ResponseResult.Fail<UserDto>("User not found"));

            return Ok(ResponseResult.Success(getUser.ToUserDto(), "Role updated"));
        }
    }
}