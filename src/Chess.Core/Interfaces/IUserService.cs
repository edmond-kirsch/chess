using Chess.Core.DomainModels;

namespace Chess.Core.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(string username, string email, string password);
    Task<UserModel?> GetUserAsync(string username);
    Task<JwtTokenModel?> GetUserTokenAsync(string username, string password);
}