namespace PolicyManagementSystem.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message = "Unauthorized access") 
            : base("UNAUTHORIZED", message, StatusCodes.Status401Unauthorized)
        {
        }
    }
}
