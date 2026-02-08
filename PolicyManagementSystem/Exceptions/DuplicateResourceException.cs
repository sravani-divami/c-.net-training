namespace PolicyManagementSystem.Exceptions
{
    public class DuplicateResourceException : BaseException
    {
        public DuplicateResourceException(string message) 
            : base("DUPLICATE_RESOURCE", message, StatusCodes.Status409Conflict)
        {
        }
    }
}
