namespace LibraryProject.Domain.Exceptions
{
    public class NoLoanException : Exception
    {

        protected NoLoanException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public NoLoanException(string message) : base(message)
        {
        }
    }
}
