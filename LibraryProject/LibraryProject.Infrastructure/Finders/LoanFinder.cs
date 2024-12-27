using Microsoft.EntityFrameworkCore;
using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Infrastructure.Context;

namespace LibraryProject.Infrastructure.Finders
{
    public class LoanFinder : ILoanFinder
    {
        private readonly PersistenceContext _dbContext;

        public LoanFinder(PersistenceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _dbContext.Loan.AsNoTracking().ToListAsync();
        }

        public async Task<Loan> GetByIdAsync(Guid id)
        {
            return await _dbContext.Loan.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Loan> GetByUserIdAsync(Guid isbn, string userId)
        {
            return await _dbContext.Loan.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId || x.Isbn == isbn);
        }
    }
}