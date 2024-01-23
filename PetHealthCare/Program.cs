using Microsoft.EntityFrameworkCore;
using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Repository;
using PetHealthCare.Repository.Impl;
using PetHealthCare.Services;
using PetHealthCare.Services.Impl;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PetDbContext>(options =>
    options.UseSqlServer(
            builder.Configuration.GetConnectionString("PetAppContext"))
        .UseLazyLoadingProxies()
        .EnableSensitiveDataLogging()
);

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPetService, PetService>();

builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPetRepository, PetRepository>();

//builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBaseImpl<>))
//    .AddTransient<IRoleRepository, RoleRepository>()
//    .AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();