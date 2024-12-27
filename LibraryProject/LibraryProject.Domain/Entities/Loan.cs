using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Domain.Entities
{
    public class Loan
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Isbn { get; set; }
        public string UserId { get; set; }
        public UserType UserType { get; set; } 
        public DateTime ReturnDate { get; set; }
    }
}