using AutoMapper;
using MediatR;
using LibraryProject.Application.Models;
using LibraryProject.Domain.Interfaces;

namespace LibraryProject.Application.Queries
{
    public class GetLoanQueryHandler : IRequestHandler<GetLoanQuery, LoanViewModel>
    {
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;
        public GetLoanQueryHandler(ILoanService loanService, IMapper mapper)
        {
            _loanService = loanService;
            _mapper = mapper;
        }

        public async Task<LoanViewModel> Handle(GetLoanQuery request, CancellationToken cancellationToken)
        {
            var loan = await _loanService.GetLoanByIdAsync(request.LoanId);
            return _mapper.Map<LoanViewModel>(loan);
        }
    }
}