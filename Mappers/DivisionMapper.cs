using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Division;
using go_han.Models;

namespace go_han.Mappers
{
    public static class DivisionMapper
    {
        public static DivisionDTO ToDivisionDTO(this Division division)
        {
            return new DivisionDTO
            {
                Id = division.Id,
                DivisionName = division.DivisionName
            };
        }
        public static Division ToDivisionRequestDTO(this CreateRequestDivisionDTO division)
        {
            return new Division
            {
                DivisionName = division.DivisionName
            };
        }
    }
}