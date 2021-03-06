var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
});

builder.Services
       .AddAuthentication(options =>
       {
           options.DefaultScheme = "Cookies";
           options.DefaultChallengeScheme = "oidc";
       })
       .AddCookie("Cookies")
       .AddOpenIdConnect("oidc", options =>
       {
           options.Authority = builder.Configuration.GetValue<string>("Oidc:Authority");
           options.ClientId = builder.Configuration.GetValue<string>("Oidc:ClientId");
           options.ClientSecret = builder.Configuration.GetValue<string>("Oidc:ClientSecret");
           options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Oidc:RequireHttpsMetadata");
           options.ResponseType = "code";
           options.SaveTokens = true;

           options.Events.OnRedirectToIdentityProvider = context =>
           {
               context.ProtocolMessage.IssuerAddress =
                   $"{builder.Configuration.GetValue<string>("Oidc:IdentityProviderUrl")}/connect/authorize";

               context.ProtocolMessage.RedirectUri =
                   $"{builder.Configuration.GetValue<string>("Oidc:ChessStatisticsApi")}/signin-oidc";

               return Task.CompletedTask;
           };
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();