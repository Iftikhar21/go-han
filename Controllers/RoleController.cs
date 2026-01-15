using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs;
using go_han.DTOs.Roles;
using go_han.Mappers.Roles;
using go_han.Repositories.IRepository;
using go_han.Repositories;
using go_han.Utils;
using Microsoft.AspNetCore.Mvc;
using go_han.Models;
using Microsoft.AspNetCore.Authorization;

namespace go_han.Controllers.Roles
{
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _repository;

        public RoleController(IRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<RoleReadDto>>>> GetAll()
        {
            var roles = await _repository.GetRolesAsync();
            var dto = roles.Select(RoleMapper.ToReadDto).ToList();
            return Ok(ResponseResult.Success(dto, "Roles retrieved successfully."));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<RoleReadDto>>> GetById(int id)
        {
            var role = await _repository.GetRoleByIdAsync(id);
            if (role is null)
                return NotFound(ResponseResult.Fail<RoleReadDto>("Role not found."));

            var roleDto = RoleMapper.ToReadDto(role);
            return Ok(ResponseResult.Success(roleDto, "Role retrieved successfully."));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<RoleReadDto>>> Create([FromBody]RoleCreateDto dto)
        {
            var entity = RoleMapper.ToEntity(dto);
            var created = await _repository.CreateRoleAsync(entity);

            if (created is null)
                return BadRequest(ResponseResult.Fail<RoleReadDto>("RoleName already exists."));
            
            var resultDto = RoleMapper.ToReadDto(created);
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = resultDto.Id },
                ResponseResult.Success(resultDto, "Role created successfully.")
            );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<RoleReadDto>>> Update(int id, [FromBody]RoleUpdateDto dto)
        {
            var entity = new Role {RoleName = dto.RoleName};

            var updated = await _repository.UpdateRoleAsync(id, entity);
            if (updated is null)
                return Conflict(ResponseResult.Fail<RoleReadDto>("Role not found or RoleName already exists."));

            var resultDto = RoleMapper.ToReadDto(updated);
            return Ok(ResponseResult.Success(resultDto, "Role updated successfully."));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deleted = await _repository.DeleteRoleAsync(id);
            if (!deleted)
                return NotFound(ResponseResult.Fail<string>("Role not found."));

            return Ok(ResponseResult.Success("OK", "Role deleted successfully."));
        }
    }
}