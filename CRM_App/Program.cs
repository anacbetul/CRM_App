using CRM_App_Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CRM_App_Repository;
using SharedLibrary.Configurations;
using CRM_App_Core.Services;
using CRM_App_Service.Services;
using CRM_App_Core.Repositories;
using CRM_App_Repository.Repositories;
using CRM_App_Core.UnitOfWork;
using Quartz;
using CRM_App_Service.Quartz;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CRM_App_Core.DTOs;
using Microsoft.SqlServer.Management.Smo.Wmi;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"), option =>
    {
        option.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
    }
));

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<List<ClientInfo>>(builder.Configuration.GetSection("Clients"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOptions = builder.Configuration.GetSection("Jwt").Get<CustomTokenOption>();
    if (tokenOptions == null)
    {
        throw new Exception("Jwt settings are missing in configuration.");
    }
    opts.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IServiceGeneric<,>), typeof(GenericService<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<CheckMeetingsJob>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    //Register your job
    q.ScheduleJob<CheckMeetingsJob>(trigger => trigger
        .WithIdentity("CheckMeetingsJobTrigger")

        .StartNow() // Start immediately
                    .WithCronSchedule("0/30 * * * * ?")); // Her 30 saniyede bir çalýþtýrýr
                  //.WithCronSchedule("0 0 11 * * ?")); // Bu cron ifadesi her gün saat 9'da çalýþtýrýr
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

var credential = GoogleCredential.FromFile("C:\\Users\\betul\\Desktop\\CRM_App\\CRM_App\\CRM_App\\myapplication-53592-firebase-adminsdk-wwpqf-2df7797369.json");
FirebaseApp.Create(new AppOptions
{
    Credential = credential
});


//builder.Services.ConfigureOptions<CheckMeetingsJob>();

// Quartz Hosted Service to run Quartz as a background service

builder.Services.AddControllersWithViews();


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();//jwt authentication middleware adding
app.UseAuthorization();

app.MapControllers();

app.Run();


