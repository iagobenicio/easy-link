using easy_link.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using easy_link.DTOs;
using easy_link.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using easy_link.Repositories;

var builder = WebApplication.CreateBuilder(args);

var config = new MapperConfiguration(cfg => 
{
    cfg.CreateMap<UserDTO,User>();
});
IMapper mapper = config.CreateMapper();

// Add services to the container.
builder.Services.AddDbContext<EasylinkContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User,IdentityRole<int>>( cfg => 
{
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireUppercase = false;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<EasylinkContext>().AddDefaultTokenProviders();

builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ILinkRepository,LinkRepository>();
builder.Services.AddScoped<IPageRepository,PageRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
        {   
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("[JWT:Key]"))
        };
    }
);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
