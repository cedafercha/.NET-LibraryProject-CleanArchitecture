using LibraryProject.Domain.Entities;

namespace LibraryProject.Domain.Interfaces
{
    public interface ILoanService
    {
        Task<Loan> NewLoanAsync(Guid isbn, string userId, UserType userType);
        Task<Loan> GetLoanByIdAsync(Guid id);
        Task<List<Loan>> GetAllAsync();
    }
}