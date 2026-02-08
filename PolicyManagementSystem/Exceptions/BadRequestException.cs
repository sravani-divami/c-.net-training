namespace PolicyManagementSystem.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) 
            : base("BAD_REQUEST", message, StatusCodes.Status400BadRequest)
        {
        }
    }
}
