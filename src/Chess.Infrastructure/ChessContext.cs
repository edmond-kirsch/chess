using Chess.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure;

public class ChessContext : IdentityDbContext<User>
{
    public ChessContext(DbContextOptions<ChessContext> options) : base(options)  
    {  
  
    }  
    protected override void OnModelCreating(ModelBuilder builder)  
    {  
        base.OnModelCreating(builder);  
    }  
}