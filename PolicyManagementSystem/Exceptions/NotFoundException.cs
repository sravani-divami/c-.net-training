namespace PolicyManagementSystem.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string resource) 
            : base("RESOURCE_NOT_FOUND", $"{resource} not found", StatusCodes.Status404NotFound)
        {
        }
    }
}
