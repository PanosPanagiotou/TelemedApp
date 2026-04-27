using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using TelemedApp.API.Authorization;
using TelemedApp.API.Middleware;
using TelemedApp.API.Validation;
using TelemedApp.Application.Interfaces;
using TelemedApp.Application.Mappings;
using TelemedApp.Application.UseCases.Appointments;
using TelemedApp.Application.UseCases.Doctors;
using TelemedApp.Application.UseCases.Patients;
using TelemedApp.Application.Validation;
using TelemedApp.Domain.Interfaces;
using TelemedApp.Identity.Authorization;
using TelemedApp.Identity.Data;
using TelemedApp.Identity.Interfaces;
using TelemedApp.Identity.Models;
using TelemedApp.Identity.Services;
using TelemedApp.Infrastructure.Data;
using TelemedApp.Infrastructure.Repositories;
using TelemedApp.Infrastructure.Services;
using TelemedApp.Shared.Authorization;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

// ---------------------------------------------------------
// DATABASES
// ---------------------------------------------------------

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddDbContext<TelemedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TelemedConnection")));

// ---------------------------------------------------------
// IDENTITY
// ---------------------------------------------------------

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

    options.Password.RequiredLength = 12;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
});

// ---------------------------------------------------------
// INFRASTRUCTURE
// ---------------------------------------------------------

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();

// ---------------------------------------------------------
// APPLICATION SERVICES
// ---------------------------------------------------------

builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// COMMAND HANDLERS (CQRS Write Side)
builder.Services.AddScoped<CreateDoctorHandler>();
builder.Services.AddScoped<UpdateDoctorHandler>();
builder.Services.AddScoped<DeleteDoctorHandler>();

builder.Services.AddScoped<CreatePatientHandler>();
builder.Services.AddScoped<UpdatePatientHandler>();
builder.Services.AddScoped<DeletePatientHandler>();

builder.Services.AddScoped<CreateAppointmentHandler>();
builder.Services.AddScoped<UpdateAppointmentHandler>();
builder.Services.AddScoped<DeleteAppointmentHandler>();

// NOTE: Find* handlers removed (queries handled by services)

// ---------------------------------------------------------
// CONTROLLERS + VALIDATION
// ---------------------------------------------------------

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Register ALL validators from TelemedApp.Application
builder.Services.AddValidatorsFromAssembly(typeof(AppointmentDtoValidator).Assembly);

// Register custom validation problem details factory
builder.Services.AddSingleton<ProblemDetailsFactory, CustomValidationProblemDetailsFactory>();

// Optional - Do not really need the following:
// builder.Services.AddOpenApi();
// Because we are using:
// builder.Services.AddSwaggerGen();
// builder.Services.AddEndpointsApiExplorer();

// ---------------------------------------------------------
// CORS
// ---------------------------------------------------------

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy.WithOrigins(allowedOrigins!)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ---------------------------------------------------------
// AUTOMAPPER
// ---------------------------------------------------------

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ---------------------------------------------------------
// JWT AUTHENTICATION
// ---------------------------------------------------------

builder.Services.AddScoped<ITokenService, TokenService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

// ---------------------------------------------------------
// AUTHORIZATION POLICIES
// ---------------------------------------------------------

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

// ---------------------------------------------------------
// SWAGGER
// ---------------------------------------------------------

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TelemedApp.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
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
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ---------------------------------------------------------
// BUILD APP
// ---------------------------------------------------------

var app = builder.Build();

// ---------------------------------------------------------
// APPLY DATABASE MIGRATIONS AUTOMATICALLY
// ---------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Identity DB
    var identityDb = services.GetRequiredService<IdentityDbContext>();
    identityDb.Database.Migrate();

    // Telemed DB
    var telemedDb = services.GetRequiredService<TelemedDbContext>();
    telemedDb.Database.Migrate();
}

// ---------------------------------------------------------
// SEED ADMIN + ROLES
// ---------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.MapControllers().WithOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowUI");
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Starting TelemedApp API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "TelemedApp API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}