using AutoMapper;
using LibraryProject.Application.Models;
using LibraryProject.Domain.Entities;

namespace LibraryProject.Application.Profiles
{
    public class LoanProfile : Profile
    {
        public LoanProfile() 
        {
            CreateMap<Loan, LoanViewModel>();
        }
    }
}
