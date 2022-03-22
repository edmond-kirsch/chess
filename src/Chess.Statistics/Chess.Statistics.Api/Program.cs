using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
       {
           options.DefaultScheme = "Cookies";
           options.DefaultChallengeScheme = "oidc";
       })
       .AddOpenIdConnect("oidc", options =>
       {
           options.Authority = "https://localhost:5001";
           options.ClientId = "chess-engine-statistics";
           options.ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A";
           options.ResponseType = "code";
           options.SaveTokens = true;
       });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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