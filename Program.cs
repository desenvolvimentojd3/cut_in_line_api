using System.Data;
using System.Text;
using CutInLine;
using CutInLine.Models.GlobalException;
using CutInLine.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

//Ler a String de conexão do appsettings.json
var connectionString = config["DataBase"];
var key = Encoding.UTF8.GetBytes(config["JwtSettings:Key"]);

builder.Services.AddScoped<IDbConnection>((_) => new NpgsqlConnection(connectionString));

//Jwt
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});


// Add services to the container.
builder.Services.AddCors(options => options.AddPolicy("AllowAnyOrigin",
                builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true))
            );
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkImplementation>();
builder.Services.AddRepositoryDepencyInjection();
builder.Services.AddInterfaceDepencyInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalExceptionMiddleware();

app.Run();
