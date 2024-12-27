using LibraryProject.Domain.Entities;

namespace LibraryProject.Domain.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan> CreateAsync(Loan loan);
    }
}