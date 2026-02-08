using PolicyManagementSystem.DTOs.Auth;
using PolicyManagementSystem.Entities;
using PolicyManagementSystem.Exceptions;
using PolicyManagementSystem.Repositories.Interfaces;
using PolicyManagementSystem.Services.Interfaces;
using PolicyManagementSystem.Utils;

namespace PolicyManagementSystem.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task RegisterAsync(RegisterRequestDto registerDto)
        {
            // Validate email uniqueness
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new DuplicateResourceException("Email already exists");
            }

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "User"
            };

            await _userRepository.AddAsync(user);
        }

        public async Task<string> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null)
            {
                throw new UnauthorizedException("Invalid email or password");
            }

            var isPasswordValid =
                BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedException("Invalid email or password");
            }

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
