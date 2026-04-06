using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using PVMS.Application.Bll;
using PVMS.Application.DI;
using PVMS.Application.Hubs;
using PVMS.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Scalar.AspNetCore;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PVMS.Application.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInnovaMapper();
builder.Services.AddSignalR();
builder.Services.AddEfDbContext(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddServices();
builder.Services.AddOptions<EmailOptions>().Bind(builder.Configuration.GetSection("Email"));
builder.Services.AddOptions<CivilStatusOptions>().Bind(builder.Configuration.GetSection("CivilStatus"));
builder.Services.AddOptions<SmsOptions>().Bind(builder.Configuration.GetSection("Sms"));
builder.Services.AddOptions<AssetsOptions>().Bind(builder.Configuration.GetSection("Assets"));
builder.Services.AddOptions<QrCodeOptions>().Bind(builder.Configuration.GetSection("QrCodeApi"));

builder.Services.AddHttpClient();
builder.Services.AddCors(op =>
{
    op.AddPolicy("AllowAllOrigins", builder =>
    builder.WithOrigins("http://192.168.0.69:4258", "http://localhost:4200") // Angular app
    .AllowAnyMethod()
    .AllowAnyHeader().
    AllowCredentials());
});
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = null;
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2L * 1024 * 1024 * 1024; // 2 GB
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 2L * 1024 * 1024 * 1024; // 2 GB
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // SignalR sends token via query string
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                path.StartsWithSegments("/notificationHub"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});
//builder.Services.AddHostedService<PVMS.Application.HostedService.BackgroundService>();
#if !DEBUG
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(1999);
});
#endif

builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 50 * 1024 * 1024; // 10MB
    options.StreamBufferCapacity = 100;
});
var app = builder.Build();
app.UseDeveloperExceptionPage();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new InnovaResponse<string>(contextFeature.Error.InnerException?.Message ?? contextFeature.Error.Message, contextFeature.Error.InnerException?.Message ?? contextFeature.Error.Message, false), new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            }));
    });
});

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.MapHub<NotificationHub>("/notificationHub");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapScalarApiReference();
app.MapOpenApi();
app.Run();


