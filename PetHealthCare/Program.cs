using Microsoft.EntityFrameworkCore;
using PetHealthCare.AppDatabaseContext;
using PetHealthCare.Config;
using PetHealthCare.Data;
using PetHealthCare.Repository;
using PetHealthCare.Repository.Impl;
using PetHealthCare.Services;
using PetHealthCare.Services.Impl;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterMapsterConfiguration();

builder.Services.AddDbContext<PetDbContext>(options =>
    options.UseSqlServer(
            builder.Configuration.GetConnectionString("PetAppContext"))
        .UseLazyLoadingProxies()
        .EnableSensitiveDataLogging()
);

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPetService, PetService>();
builder.Services.AddTransient<IOfferingsService, OfferingsService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IProvidersService, ProvidersService>();
builder.Services.AddTransient<IMembershipService, MembershipService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();

builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPetRepository, PetRepository>();
builder.Services.AddTransient<IOfferingsRepository, OfferingsRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddTransient<IProvidersRepository, ProvidersRepository>();
builder.Services.AddTransient<IMembershipRepository, MembershipRepository>();
builder.Services.AddTransient<IMemberUserRepository, MemberUserRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
//builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleRepository = services.GetRequiredService<IRoleRepository>();
    var userRepository = services.GetRequiredService<IUserRepository>();
    var membershipRepository = services.GetRequiredService<IMembershipRepository>();

    var seedData = new SeedData(roleRepository, membershipRepository, userRepository);
    seedData.Initialize();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();