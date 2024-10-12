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
var retryPipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
    .AddRetry(new HttpRetryStrategyOptions
    {
        BackoffType = DelayBackoffType.Exponential,
        MaxRetryAttempts = 3
    })
    .Build();

var socketHandler = new SocketsHttpHandler
{
    MaxConnectionsPerServer = 1000,
    PooledConnectionLifetime = TimeSpan.FromMinutes(15),
    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1)
};

#pragma warning disable EXTEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var resilienceHandler = new ResilienceHandler(retryPipeline)
{
    InnerHandler = socketHandler,
};
#pragma warning restore EXTEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

builder.Services.AddHttpClient("WebhookClient")
                .ConfigurePrimaryHttpMessageHandler(() => resilienceHandler);

//Finish configuring HTTP Client


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

app.UseAuthorization();

app.MapControllers();

app.Run();
