using CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetCustomers;
using CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts;
using CBRE.FacilityManagement.Audit.Persistence.Repository;
using Microsoft.Extensions.Configuration;
using CBRE.FacilityManagement.Audit.Infrastructure;
using CBRE.FacilityManagement.Audit.API;
using Microsoft.Extensions.Options;
using CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register AutoMapper
        builder.Services.AddAutoMapper(typeof(Program));

        // Register MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCustomerQuery).Assembly));
        builder.Services.AddScoped<IELogBookRepository, ELogBookRepository>();
        builder.Services.AddDbContext<ELogBookDbContext>();
        builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAISettings"));

        // Register IDocumentSummarizerAIService with parameters
        builder.Services.AddScoped<IDocumentSummarizerAIService>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<OpenAISettings>>().Value;
            return new DocumentSummarizerAIService(settings.Endpoint, settings.ApiKey, settings.DeploymentName);
        });

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
    }

}