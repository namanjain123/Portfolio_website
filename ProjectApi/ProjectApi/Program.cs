using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Newtonsoft.Json.Serialization;
using ProjectApi.Client;
using ProjectApi.Model;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//Add Mapper
builder.Services.AddSingleton<IMapper>(mapper => {
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new AutoMapperProfile());
    });
    return mappingConfig.CreateMapper(); 
});
//setup MongoDb server 
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DataBaseConnection");
    return new MongoClient(connectionString);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c => 
{
    c.AddPolicy("AllowOrigin", a => a.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//Json Serlializer Addition
builder.Services.AddControllersWithViews().AddNewtonsoftJson(
                a => a.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .AddNewtonsoftJson(
                b =>
                b.SerializerSettings.ContractResolver = new DefaultContractResolver()
                );
//Addition of authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "naman",
        ValidAudience = "naman@example.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Namanjain9460202770an#"))
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(a => a.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
