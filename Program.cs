using easy_link.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using easy_link.DTOs;
using easy_link.Entities;
using Microsoft.AspNetCore.Identity;

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
}).AddEntityFrameworkStores<EasylinkContext>().AddDefaultTokenProviders();
builder.Services.AddSingleton(mapper);

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
