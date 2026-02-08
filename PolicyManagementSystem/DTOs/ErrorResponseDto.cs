namespace PolicyManagementSystem.DTOs
{
    public class ErrorResponseDto
    {
        public required string ErrorCode { get; set; }
        public required string Message { get; set; }
        public required string TraceId { get; set; }
    }
}
