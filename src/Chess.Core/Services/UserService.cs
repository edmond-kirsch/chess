using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chess.Core.DomainModels;
using Chess.Core.Entities;
using Chess.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Chess.Core.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtOptions _jwtOptions;


    public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        IOptionsMonitor<ChessOptions> options)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtOptions = options.CurrentValue.Jwt;
    }

    public async Task<bool> CreateUserAsync(string username, string email, string password)
    {
        var user = new User
        {
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = username
        };

        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded;
    }

    public async Task<UserModel?> GetUserAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return null;
        }

        var userModel = new UserModel
        {
            //todo
        };

        return userModel;
    }

    public async Task<JwtTokenModel?> GetUserTokenAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null && await _userManager.CheckPasswordAsync(user, password))
        {
            return null;
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

        var token = new JwtSecurityToken(_jwtOptions.ValidIssuer, _jwtOptions.ValidAudience, authClaims, null,
            DateTime.Now.AddHours(3),
            new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        var tokenModel = new JwtTokenModel
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ValidTo = token.ValidTo
        };

        return tokenModel;
    }
}