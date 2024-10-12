using Microsoft.EntityFrameworkCore;
using WebHooks.Models;

namespace WebHooks.Database;

public class WebhookServiceContext : DbContext{
    public DbSet<Event> Events {get; set;}
    public DbSet<Webhook> Webhooks {get; set;}

    public WebhookServiceContext(DbContextOptions<WebhookServiceContext> options)
    : base(options){}

    override protected void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Event>()
                    .HasMany(e => e.Webhooks)
                    .WithOne(w => w.Event)
                    .HasForeignKey(w => w.EventId)
                    .IsRequired();

        modelBuilder.Entity<Webhook>()
                    .Property(w => w.Endpoint)
                    .HasConversion(
                        uri => uri.AbsoluteUri,
                        path => new Uri(path)
                    );
    }
}