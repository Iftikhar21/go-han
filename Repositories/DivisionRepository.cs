using go_han.Data;
using go_han.Models;
using go_han.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace go_han.Repositories.IRepository
{
    public class DivisionRepository : IDivisionRepository
    {
        private readonly AppDbContext _context;

        public DivisionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Division>> GetAllDivisionsAsync()
        {
            return await _context.Divisions.ToListAsync();
        }
        public async Task<Division?> GetDivisionByIdAsync(int id)
        {
            var existDivision = await _context.Divisions.FindAsync(id);
            if (existDivision == null)
                return null;

            return existDivision;
        }
        public async Task<Division> CreateDivisionAsync(Division division)
        {
            await _context.Divisions.AddAsync(division);
            await _context.SaveChangesAsync();

            return division;
        } 
        public async Task<Division?> UpdateDivisionAsync(int id, Division division)
        {
            var existDivision = await _context.Divisions.FindAsync(id);
            if (existDivision == null)
                return null;
            
            existDivision.DivisionName = division.DivisionName;
            await _context.SaveChangesAsync();
            return existDivision;
        }
        public async Task<bool> DeleteDivisionAsync(int id)
        {
            var existDivision = await _context.Divisions.FindAsync(id);
            if (existDivision == null)
                return false;

            _context.Divisions.Remove(existDivision);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}