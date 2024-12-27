namespace LibraryProject.Domain.Exceptions
{
    public class BookLoanException : Exception
    {

        protected BookLoanException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public BookLoanException(string message) : base(message)
        {
        }
    }
}
