using PolicyManagementSystem.Entities;

namespace PolicyManagementSystem.Utils
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}