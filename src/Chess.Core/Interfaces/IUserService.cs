using Chess.Core.DomainModels;
using JetBrains.Annotations;

namespace Chess.Core.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync([NotNull] string username, [NotNull] string email, [NotNull] string password);
    [NotNull]
    [ItemCanBeNull]
    Task<UserModel> GetUserAsync(string username);
    [NotNull]
    [ItemCanBeNull]
    Task<JwtTokenModel> GetUserTokenAsync(string username, string password);
}