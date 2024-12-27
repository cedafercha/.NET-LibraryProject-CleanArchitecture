using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Infrastructure.Context;

namespace LibraryProject.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly PersistenceContext _dbContext;

        public LoanRepository(PersistenceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Loan> CreateAsync(Loan loan)
        {
            await _dbContext.AddAsync(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }
    }
}