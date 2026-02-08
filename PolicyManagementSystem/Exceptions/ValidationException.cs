namespace PolicyManagementSystem.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message) 
            : base("VALIDATION_ERROR", message, StatusCodes.Status400BadRequest)
        {
        }
    }
}
