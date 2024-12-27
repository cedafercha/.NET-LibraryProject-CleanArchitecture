using MediatR;
using LibraryProject.Application.Models;

namespace LibraryProject.Application.Queries
{
    public class GetAllQuery : IRequest<List<LoanViewModel>>
    {}
}