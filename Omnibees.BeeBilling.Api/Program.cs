                     using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Api.Extensions;
using Omnibees.BeeBilling.Application.Extensions;
using Omnibees.BeeBilling.Application.Mappers;
using Omnibees.BeeBilling.Infrastructure.Extensions;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;
using Omnibees.BeeBilling.Infrastructure.Persistence.Seed.Configuration;

var builder = WebApplication.CreateBuilder(args);
var dbConnectionString = builder.Configuration.GetConnectionString("DBConnection") ?? string.Empty;

builder.Services.AddDbContext<BeeBillingDbContext>(options =>
{
    options.UseSqlServer(dbConnectionString);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddApiVersioningSupport();
builder.Services.AddAutoMapper(typeof(CotacaoProfile));
builder.Services
    .AddHealthChecks()
    .AddSqlServer(
        connectionString: dbConnectionString,
        name: "sql",
        tags: ["db"]
     );

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BeeBillingDbContext>();
    context.Database.Migrate();
    DbInitializer.Seed(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
