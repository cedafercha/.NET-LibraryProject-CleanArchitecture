using Xunit;
using Moq;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;

namespace Api.Test
{
    public class LoanServiceTests
    {
        private readonly Mock<ILoanFinder> _mockLoanFinder;
        private readonly Mock<ILoanRepository> _mockLoanRepository;
        private readonly LoanService _loanService;

        public LoanServiceTests ()
        {
            _mockLoanFinder = new Mock<ILoanFinder>();
            _mockLoanRepository = new Mock<ILoanRepository>();
            _loanService = new LoanService(_mockLoanFinder.Object, _mockLoanRepository.Object);
        }

        [Fact]
        public async void GetLoanByIdTest()
        {
            // Arrange
            var loanId = Guid.NewGuid();
            var expectedLoan = new Loan { Id = loanId };
            _mockLoanFinder.Setup(x => x.GetByIdAsync(loanId)).ReturnsAsync(expectedLoan);

            // Act
            var result = await _loanService.GetLoanByIdAsync(loanId);

            // Assert
            _mockLoanFinder.Verify(x => x.GetByIdAsync(loanId), Times.Once);
            result.Should().BeOfType<Loan>();
        }

        [Fact]
        public async void GetAllLoansTest()
        {
            // Arrange
            var expectedLoans = new List<Loan> {
                new Loan { Id = Guid.NewGuid() },
                new Loan { Id = Guid.NewGuid() }
            };
            _mockLoanFinder.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedLoans);

            // Act
            var result = await _loanService.GetAllAsync();

            // Assert
            _mockLoanFinder.Verify(x => x.GetAllAsync(), Times.Once);
            result.Should().BeOfType<List<Loan>>();
        }
    }
}
