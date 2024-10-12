using Microsoft.EntityFrameworkCore;
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

builder.Services.AddHttpClient();

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
