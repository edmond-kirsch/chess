using System.Text;
using Chess.Core;
using Chess.Core.Entities;
using Chess.Core.Interfaces;
using Chess.Core.Services;
using Chess.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChessContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ChessContext"), 
        b => b.MigrationsAssembly("Chess.Infrastructure")));

builder.Services.AddIdentity<User, IdentityRole>()  
       .AddEntityFrameworkStores<ChessContext>()  
       .AddDefaultTokenProviders();  

// Adding Authentication  
builder.Services.AddAuthentication(options =>  
       {  
           options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
           options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
           options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
       })  
       .AddJwtBearer(options =>  
       {  
           options.SaveToken = true;  
           options.RequireHttpsMetadata = false;  
           options.TokenValidationParameters = new TokenValidationParameters()  
           {  
               ValidateIssuer = true,  
               ValidateAudience = true,  
               ValidAudience = builder.Configuration["JWT:ValidAudience"],  
               ValidIssuer = builder.Configuration["JWT:ValidIssuer"],  
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))  
           };  
       });

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.Configure<ChessOptions>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();