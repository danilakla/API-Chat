
using API_Chat.Data;
using API_Chat.Extensions;
using API_Chat.Hubs;
using API_Chat.IntegrationEvents.EventHandling;
using API_Chat.IntegrationEvents.Events;
using Autofac.Extensions.DependencyInjection;
using EventBus.Abstructions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddCustomServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationContext>(opt =>
{
    opt.UseSqlServer(configuration["AppSettings:MSS"]);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddIntegrationServices(builder.Configuration);
builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    redisOptions.Configuration = configuration["AppSettings:Redis"];
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata= false;

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateIssuerSigningKey = false,
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            string accessToken="";
            if (context.Request.Headers.Authorization.ToString()=="")
            {

            }
            else
            {

                accessToken = context.Request.Headers.Authorization.ToString().Split(" ")[1];
            }
            var test = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/chathub")))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy("M", b =>
    {
        b
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();

        
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.Use(async (context, next) =>
//{
//    //var accessToken = context.Request.Query["access_token"];
//    var testAccessToken = context.Request.Headers.Authorization;
//    if (!string.IsNullOrEmpty(testAccessToken))
//    {
//        context.Request.Headers["Authorization"] = testAccessToken;
//    }

//    await next.Invoke().ConfigureAwait(false);
//});

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("M");
app.MapHub<Chat>("/chathub");
app.MapControllers();
ConfigureEventBus(app);
app.Run();

void ConfigureEventBus(IApplicationBuilder app)
{
	var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
	eventBus.Subscribe<CreateProfileBaseOnUniverDataIntegrationEvent, CreateContactsIntegrationEventHandler>();
}