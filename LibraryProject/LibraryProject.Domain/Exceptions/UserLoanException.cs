namespace LibraryProject.Domain.Exceptions
{
    public class UserLoanException : Exception
    {

        protected UserLoanException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public UserLoanException(string message) : base(message)
        {
        }
    }
}
