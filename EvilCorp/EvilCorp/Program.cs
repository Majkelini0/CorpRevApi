using System.Text;
using EvilCorp.Context;
using EvilCorp.Middlewares;
using EvilCorp.Services.ClientService;
using EvilCorp.Services.Login;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// // // SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY // // //
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });
    c.AddSecurityDefinition("AuthorizationBearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
          <br/> Enter your token in the text input below.
          <br/> You don't have to add prefix 'Bearer'.
          <br/> Example: 'this_is_my_token'",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "AuthorizationBearer"
                },
                Scheme = "oauth2",
                Name = "Bearer"
            },
            new List<string>()
        }
    });
});
// // // SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY SECURITY // // //


builder.Services.AddControllers();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IClientService, ClientService>();



builder.Services.AddDbContext<EvilCorpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


// // // AUTHENTICATION AUTHENTICATION AUTHENTICATION AUTHENTICATION AUTHENTICATION AUTHENTICATION // // //
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //by who
        ValidateAudience = true, //for whom
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2),
        ValidIssuer = builder.Configuration["JWT:Issuer"], //should come from configuration
        ValidAudience = builder.Configuration["JWT:Audience"], //should come from configuration
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };

    opt.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-expired", "true");
            }

            return Task.CompletedTask;
        }
    };
}).AddJwtBearer("IgnoreTokenExpirationScheme", opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //by who
        ValidateAudience = true, //for whom
        ValidateLifetime = false,
        ClockSkew = TimeSpan.FromMinutes(2),
        ValidIssuer = builder.Configuration["JWT:Issuer"], //should come from configuration
        ValidAudience = builder.Configuration["JWT:Audience"], //should come from configuration
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});
// // // AUTHENTICATION AUTHENTICATION AUTHENTICATION AUTHENTICATION AUTHENTICATION AUTHENTICATION // // //



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
//app.UseAuthentication();
//app.UseAuthorization();

app.Run("http://localhost:5555");
