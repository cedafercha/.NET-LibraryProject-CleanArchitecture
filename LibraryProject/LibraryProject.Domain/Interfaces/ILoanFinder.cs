using LibraryProject.Domain.Entities;

namespace LibraryProject.Domain.Interfaces
{
    public interface ILoanFinder
    {
        Task<Loan> GetByIdAsync(Guid id);
        Task<Loan> GetByUserIdAsync(Guid isbn, string userId);
        Task<List<Loan>> GetAllAsync();
    }
}