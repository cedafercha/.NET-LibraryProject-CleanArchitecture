using AutoMapper;
using LibraryProject.Application.Models;
using LibraryProject.Domain.Entities;

namespace LibraryProject.Application.Profiles
{
    public class NewLoanProfile : Profile
    {
        public NewLoanProfile() 
        {
            CreateMap<Loan, NewLoanViewModel>();
        }
    }
}
