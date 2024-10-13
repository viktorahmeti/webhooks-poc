using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using WebHooks.Client;
using WebHooks.Database;
using WebHooks.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IWebhookService, WebhookService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddSingleton<IWebhookHttpClient, WebhookHttpClient>();

builder.Services.AddDbContext<WebhookServiceContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDatabase"));
});


//configure HTTP Client as per guidelines: https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
builder.Services.AddHttpClient("WebhookClient")
                .AddStandardResilienceHandler()
                .Configure(o => {
                    o.Retry = new HttpRetryStrategyOptions
                        {
                            BackoffType = DelayBackoffType.Exponential,
                            MaxRetryAttempts = 3
                        };
                });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Disable CORS for Development, to communicate with frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
