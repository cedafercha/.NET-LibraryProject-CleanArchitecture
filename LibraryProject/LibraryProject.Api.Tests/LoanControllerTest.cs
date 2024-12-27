using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using Xunit;
using FluentAssertions;

namespace Api.Test
{
    public class LoanControllerTest : IntegrationTestBuilder
    {
        [Fact]
        public void GetSuccessfulLoan()
        {
            // Arrange
            var loanRequest = new
            {
                UserType = USerType.Guest,
                UserId = "123456789",
                Isbn = Guid.NewGuid()
            };

            // Act
            var result = this.TestClient.PostAsync("/api/loan", loanRequest, new JsonMediaTypeFormatter()).Result;
            var data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(result.Content.ReadAsStringAsync().Result);
            
            var loanId = data["id"];
            var c = this.TestClient.GetAsync($"api/loan/{loanId}").Result;
            c.EnsureSuccessStatusCode();

            var response = c.Content.ReadAsStringAsync().Result;
            var responseData = System.Text.Json.JsonSerializer.Deserialize<ResponseDto>(response);

            // Assert
            responseData.UserId.Should().Be(loanRequest.UserId);
            responseData.Isbn.Should().Be(loanRequest.Isbn);
        }

        [Fact]
        public void GetLoanError()
        {
            // Arrange
            var loanId = Guid.NewGuid().ToString();
            HttpResponseMessage response = null;

            // Act
            try
            {
                response = this.TestClient.GetAsync($"api/loan/{loanId}").Result;
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public void PostUserLoanError()
        {
            // Arrange
            var userId = "1234567890";
            var errorMessage = $"The user with id {userId} already has a book on loan, so another loan cannot be made.";
            HttpResponseMessage response = null;

            // Act
            try
            {
                var loanRequest = new 
                {
                    UserType = USerType.Guest,
                    UserId = userId,
                    Isbn = Guid.NewGuid()
                };

                response = this.TestClient.PostAsync("api/loan", loanRequest, new JsonMediaTypeFormatter()).Result;
                response.EnsureSuccessStatusCode();

                response = this.TestClient.PostAsync("api/loan", loanRequest, new JsonMediaTypeFormatter()).Result;
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                // Assert
                var responseContent = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                responseContent["message"].Should().Be(errorMessage);
            }
        }

        [Fact]
        public void PostGuestLoanReturnDateSuccesful()
        {
            // Arrange
            var loanRequest = new 
            {
                UserType = USerType.Guest,
                UserId = "951263584",
                Isbn = Guid.NewGuid()
            };
            var expectedDate = GetReturnDate(USerType.Guest).ToShortDateString();

            // Act
            var response = this.TestClient.PostAsync("api/loan", loanRequest, new JsonMediaTypeFormatter()).Result;
            response.EnsureSuccessStatusCode();

            var newLoan = System.Text.Json.JsonSerializer
               .Deserialize<Dictionary<string, object>>(response.Content.ReadAsStringAsync().Result);
            var returnDate = DateTime.Parse(newLoan["returnDate"].ToString()).ToShortDateString();

            // Assert
            returnDate.Should().Be(expectedDate);
        }

        [Fact]
        public void PostEmployeeLoanReturnDateSuccesful()
        {
            // Arrange
            var loanRequest = new 
            {
                UserType = USerType.Employee,
                UserId = "9876543210",
                Isbn = Guid.NewGuid()
            };
            var expectedDate = GetReturnDate(USerType.Employee).ToShortDateString();

            // Act
            var response = this.TestClient.PostAsync("api/loan", loanRequest, new JsonMediaTypeFormatter()).Result;
            response.EnsureSuccessStatusCode();

            var newLoan = System.Text.Json.JsonSerializer
                .Deserialize<Dictionary<string, object>>(response.Content.ReadAsStringAsync().Result);
            var returnDate = DateTime.Parse(newLoan["returnDate"].ToString()).ToShortDateString();

            // Assert
            returnDate.Should().Be(expectedDate);
        }

        [Fact]
        public void PostAffiliatedLoanReturnDateSuccesful()
        {           
            // Arrange
            var loanRequest = new 
            {
                UserType = USerType.Affiliated,
                UserId = "1234568",
                Isbn = Guid.NewGuid()
            };
            var expectedDate = GetReturnDate(USerType.Affiliated).ToShortDateString();

            // Act
            var response = this.TestClient.PostAsync("api/loan", loanRequest, new JsonMediaTypeFormatter()).Result;
            response.EnsureSuccessStatusCode();

            var newLoan = System.Text.Json.JsonSerializer
                .Deserialize<Dictionary<string, object>>(response.Content.ReadAsStringAsync().Result);
            var returnDate = DateTime.Parse(newLoan["returnDate"].ToString()).ToShortDateString();

            // Assert
            returnDate.Should().Be(expectedDate);
        }

        [Fact]
        public void PostAffiliatedLoanIsbnError()
        {
            // Arrange
            HttpResponseMessage response = null;
            try
            {
                var loanRequest = new
                {
                    UserType = USerType.Affiliated,
                    UserId = Guid.NewGuid().ToString(),
                    Isbn = "ASDFG123456789"
                };
                var expectedDate = GetReturnDate(USerType.Affiliated).ToShortDateString();

                // Act
                response = this.TestClient.PostAsync("api/loan", loanRequest, new JsonMediaTypeFormatter()).Result;
                response.EnsureSuccessStatusCode();

                Assert.True(false, "Should not return success, wrong payload");
            }
            catch (Exception)
            {         
                // Assert       
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public void PostAffiliatedLoanUserTypeError()
        {
            // Arrange
            HttpResponseMessage response = null;
            try
            {
                var loanRequest = new
                {
                    UserType = 5,
                    UserId = Guid.NewGuid().ToString(),
                    Isbn = Guid.NewGuid().ToString()
                };
                var expectedDate = GetReturnDate(USerType.Affiliated).ToShortDateString();

                // Act
                response = this.TestClient.PostAsync("api/loan", loanRequest, new JsonMediaTypeFormatter()).Result;
                response.EnsureSuccessStatusCode();

                Assert.True(false,"Should not return success, wrong payload");
            }
            catch (Exception)
            {
                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public void GetErrorWhenUserTypeIsInvalid()
        {
            // Arrange
            var loanRequest = new
            {
                UserType = 99,
                UserId = "123456789",
                Isbn = Guid.NewGuid()
            };
            // Act
            HttpResponseMessage response = TestClient.PostAsync("api/loan", loanRequest, new JsonMediaTypeFormatter()).Result;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void GetAllLoans()
        {
            // Arrange
            var loanRequest = new
            {
                UserType = USerType.Guest,
                UserId = "123456789",
                Isbn = Guid.NewGuid()
            };
            var loanRequest2 = new
            {
                UserType = USerType.Affiliated,
                UserId = "1111",
                Isbn = Guid.NewGuid()
            };

            // Act
            this.TestClient.PostAsync("/api/loan", loanRequest, new JsonMediaTypeFormatter());
            this.TestClient.PostAsync("/api/loan", loanRequest2, new JsonMediaTypeFormatter());

            var c = this.TestClient.GetAsync($"api/loan").Result;
            c.EnsureSuccessStatusCode();

            var response = c.Content.ReadAsStringAsync().Result;
            var requestResponse = System.Text.Json.JsonSerializer.Deserialize<List<ResponseDto>>(response);

            // Assert
            requestResponse.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async void PostBookLoanError()
        {
            // Arrange
            var userId = "1234567890";
            var userId2 = "1234567891";
            var isbn = Guid.NewGuid();
            var errorMessage = $"The book with id {isbn} is already on loan.";
            HttpResponseMessage response = null;

            // Act
            try
            {
                var loanRequest = new 
                {
                    UserType = USerType.Guest,
                    UserId = userId,
                    Isbn = isbn
                };

                var loanRequest2 = new 
                {
                    UserType = USerType.Guest,
                    UserId = userId2,
                    Isbn = isbn
                };

                await this.TestClient.PostAsync("api/loan", loanRequest, new JsonMediaTypeFormatter());

                response = this.TestClient.PostAsync("api/loan", loanRequest2, new JsonMediaTypeFormatter()).Result;
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                // Assert
                var responseContent = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                responseContent["message"].Should().Be(errorMessage);
            }
        }
    }
}
