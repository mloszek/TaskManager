using TaskManager.Entities;

namespace TaskManager.Identity
{
    public interface IJwtProvider
    {
        string GenerateJwtToken(User user);
    }
}
