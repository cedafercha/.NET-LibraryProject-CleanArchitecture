using AutoMapper;
using MediatR;
using LibraryProject.Application.Models;
using LibraryProject.Domain.Interfaces;

namespace LibraryProject.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<NewLoanCommand, NewLoanViewModel>
    {
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(ILoanService loanService, IMapper mapper)
        {
            _loanService = loanService;
            _mapper = mapper;
        }

        public async Task<NewLoanViewModel> Handle(NewLoanCommand request, CancellationToken cancellationToken)
        {
            var newLoan = await _loanService.NewLoanAsync(request.Isbn, request.UserId, request.UserType);
            return _mapper.Map<NewLoanViewModel>(newLoan);
        }
    }
}