using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Api.Test
{
    public abstract class IntegrationTestBuilder : IDisposable
    {
        protected HttpClient TestClient;
        public enum USerType { Affiliated = 1, Employee, Guest }
        private bool Disposed;

        protected IntegrationTestBuilder(){
            BootstrapTestingSuite();
        }

        protected void BootstrapTestingSuite()
        {
            Disposed = false;
            var appFactory = new WebApplicationFactory<LibraryProject.Api.Startup>();                                   
            TestClient = appFactory.CreateClient();
        }
       

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                TestClient.Dispose();
            }

            Disposed = true;
        }

        public DateTime GetReturnDate(USerType userType)
        {
            var weekend = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            var returnDate = DateTime.Now;
            int loanDays = userType switch
            {
                USerType.Affiliated => 10,
                USerType.Employee => 8,
                USerType.Guest => 7,
                _ => -1,
            };

            for (int i = 0; i < loanDays;)
            {
                returnDate = returnDate.AddDays(1);
                i = (!weekend.Contains(returnDate.DayOfWeek)) ? ++i : i;                
            }

            return returnDate;
        }


    }

    public  class ResponseDto
    {
        [JsonPropertyName("id")]
        public Guid bookId { get; set; }
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("isbn")]
        public Guid Isbn { get; set; }
        [JsonPropertyName("userType")]
        public int UserType { get; set; }
        [JsonPropertyName("returnDate")]
        public string ReturnDate { get; set; }           
    }
}
