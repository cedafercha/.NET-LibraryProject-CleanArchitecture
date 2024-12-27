using MediatR;
using LibraryProject.Application.Models;

namespace LibraryProject.Application.Queries
{
    public class GetLoanQuery : IRequest<LoanViewModel>
    {
        public Guid LoanId { get; set; }
    }
}