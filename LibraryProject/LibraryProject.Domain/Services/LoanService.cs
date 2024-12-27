using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Exceptions;
using LibraryProject.Domain.Interfaces;

namespace LibraryProject.Domain.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanFinder _loanFinder;
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanFinder loanFinder, ILoanRepository loanRepository)
        {
            _loanFinder = loanFinder;
            _loanRepository = loanRepository;
        }

        public Task<List<Loan>> GetAllAsync()
        {
            return _loanFinder.GetAllAsync();
        }

        public async Task<Loan> GetLoanByIdAsync(Guid id)
        {
            var loan = await _loanFinder.GetByIdAsync(id);

            if (loan is null)
            {
                throw new NoLoanException(id.ToString());
            }

            return loan;
        }

        public async Task<Loan> NewLoanAsync(Guid isbn, string userId, UserType userType)
        {
            await ValidateCurrentLoan(isbn, userId);
            
            var newLoan = new Loan
            {
                Id = Guid.NewGuid(),
                Isbn = isbn,
                UserId = userId,
                UserType = userType,
                ReturnDate = GetLoanMaxDate(userType)
            };

            return await _loanRepository.CreateAsync(newLoan);
        }

        private async Task ValidateCurrentLoan(Guid isbn, string userId)
        {
            var currentLoan = await _loanFinder.GetByUserIdAsync(isbn, userId);

            if(currentLoan is not null)
            {
                if(currentLoan.Isbn == isbn)
                {
                    throw new BookLoanException(isbn.ToString());
                }

                throw new UserLoanException(userId);
            }
        }

        private DateTime GetLoanMaxDate(UserType userType)
        {
            Dictionary<UserType, int> userTypeDays = new Dictionary<UserType, int>(){
                { UserType.Affiliated, 10 },
                { UserType.Employee, 8 },
                { UserType.Guest, 7 },
            };
            DateTime today = DateTime.Now.Date;
            DateTime newDate = today;
            int dayAdded = 0;

            while (dayAdded < userTypeDays.GetValueOrDefault(userType, -1))
            {
                newDate = newDate.AddDays(1);

                if (newDate.DayOfWeek != DayOfWeek.Saturday && newDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    dayAdded++;
                }
            }

            return newDate;
        }
    }
}