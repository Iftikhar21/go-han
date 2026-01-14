using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Models;

namespace go_han.Repositories.IRepository
{
    public interface IDivisionRepository
    {
        public Task<List<Division>> GetAllDivisionsAsync();
        public Task<Division?> GetDivisionByIdAsync(int id);
        public Task<Division> CreateDivisionAsync(Division division);
        public Task<Division?> UpdateDivisionAsync(int id, Division division);
        public Task<bool> DeleteDivisionAsync(int id);
    }
}