using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Division;
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
    [Route("api/divisions")]
    public class DivisionController : ControllerBase
    {
        private readonly IDivisionRepository _repository;
        public DivisionController(IDivisionRepository divisionRepository)
        {
            _repository = divisionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDivisions()
        {
            var divisions = await _repository.GetAllDivisionsAsync();
            if (divisions == null)
                return Ok(ResponseResult.Fail<List<Division>>("Divisions not found"));

            var divisionDTO = divisions.Select(d => d.ToDivisionDTO()).ToList();
            return Ok(ResponseResult.Success(divisionDTO, "All divisions list"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDivisionById(int id)
        {
            var division = await _repository.GetDivisionByIdAsync(id);
            if (division == null)
                return NotFound(ResponseResult.Fail<List<Division>>($"Division with id: {id} not found"));

            var divisionDTO = division.ToDivisionDTO();
            return Ok(ResponseResult.Success(divisionDTO, $"Division with id: {id} found"));
        }
        [HttpPost]
        public async Task<IActionResult> CreateDivision([FromBody] CreateRequestDivisionDTO dto)
        {
            var division = dto.ToDivisionRequestDTO();
            var divisionDTO = await _repository.CreateDivisionAsync(division);
            if (divisionDTO == null)
                return BadRequest(ResponseResult.Fail<List<Division>>("Failed to create division"));

            return CreatedAtAction(
                nameof(GetDivisionById),
                new { id = divisionDTO.Id },
                ResponseResult.Success<DivisionDTO>(
                    divisionDTO.ToDivisionDTO(),
                    "Division created successfully")
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDivison(int id, [FromBody] CreateRequestDivisionDTO dto)
        {
            var division = dto.ToDivisionRequestDTO();
            var divisionDTO = await _repository.UpdateDivisionAsync(id, division);
            if (divisionDTO == null)
                return BadRequest(ResponseResult.Fail<List<Division>>($"Division with id: {id} not found"));

            return CreatedAtAction(
                nameof(GetDivisionById),
                new { id = divisionDTO.Id },
                ResponseResult.Success<DivisionDTO>(
                    divisionDTO.ToDivisionDTO(),
                    $"Division with id: {id} updated successfully")
            );
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDivision(int id)
        {
            var division = await _repository.DeleteDivisionAsync(id);
            if (division == false)
                return NotFound(ResponseResult.Fail<List<Division>>($"Division with id: {id} not found"));

            return Ok(ResponseResult.Success<DivisionDTO>(null, $"Division with id: {id} deleted successfully"));
        }
    }
}