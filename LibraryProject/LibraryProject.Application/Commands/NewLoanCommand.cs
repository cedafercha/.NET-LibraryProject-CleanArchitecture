using System.ComponentModel.DataAnnotations;
using MediatR;
using LibraryProject.Application.Models;

namespace LibraryProject.Application.Commands
{
    public class NewLoanCommand : IRequest<NewLoanViewModel>
    {
        [Required]
        public Guid Isbn { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "The field must be between 1 and 10 characters.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "The field must contain only letters and numbers.")]
        public string UserId { get; set; }

        [Required]
        [EnumDataType(typeof(UserType), ErrorMessage = "The field must be a valid UserType (1, 2 or 3).")]
        public UserType UserType { get; set; } 
    }
}