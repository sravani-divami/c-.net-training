namespace PolicyManagementSystem.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message = "You do not have permission to perform this action") 
            : base("FORBIDDEN", message, StatusCodes.Status403Forbidden)
        {
        }
    }
}
