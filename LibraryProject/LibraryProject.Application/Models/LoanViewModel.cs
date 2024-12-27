namespace LibraryProject.Application.Models
{
    public class LoanViewModel
    {
        public Guid Id { get; set; }
        public Guid Isbn { get; set; }
        public string UserId { get; set; }
        public UserType UserType { get; set; } 
        public DateTime ReturnDate { get; set; }
    }
}