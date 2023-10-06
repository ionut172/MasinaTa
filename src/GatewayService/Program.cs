using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
        options.TokenValidationParameters.ValidAudience = "nextapp";

        IdentityModelEventSource.ShowPII = true;
        
    });
builder.Services.AddCors(options => 
{
    options.AddPolicy("customPolicy", b => 
    {
        b.AllowAnyHeader()
            .AllowAnyMethod().AllowCredentials().WithOrigins("nextApp");
    });
});
var app = builder.Build();
app.UseCors();


app.MapReverseProxy();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
