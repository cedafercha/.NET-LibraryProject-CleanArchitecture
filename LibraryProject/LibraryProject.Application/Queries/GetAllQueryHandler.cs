using AutoMapper;
using MediatR;
using LibraryProject.Application.Models;
using LibraryProject.Domain.Interfaces;

namespace LibraryProject.Application.Queries
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<LoanViewModel>>
    {
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;
        public GetAllQueryHandler(ILoanService loanService, IMapper mapper)
        {
            _loanService = loanService;
            _mapper = mapper;
        }

        public async Task<List<LoanViewModel>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var allData = await _loanService.GetAllAsync();
            return _mapper.Map<List<LoanViewModel>>(allData);
        }
    }
}