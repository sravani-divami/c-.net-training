namespace PolicyManagementSystem.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }

        protected BaseException(string errorCode, string message, int statusCode) 
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}
