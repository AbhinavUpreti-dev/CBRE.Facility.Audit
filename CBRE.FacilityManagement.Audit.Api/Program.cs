using CBRE.FacilityManagement.Audit.Application.Contracts.Persistence;
using CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetCustomers;
using CBRE.FacilityManagement.Audit.Application.MappingProfile;
using CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts;
using CBRE.FacilityManagement.Audit.Persistence.Repository;
using Microsoft.Extensions.Configuration;
using CBRE.FacilityManagement.Audit.Infrastructure;
using CBRE.FacilityManagement.Audit.API;
using Microsoft.Extensions.Options;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCustomerQuery).Assembly));
        builder.Services.AddScoped<IELogBookRepository, ELogBookRespository>();
        // Register AutoMapper
        builder.Services.AddAutoMapper(typeof(ElogBookMappingProfile));
        builder.Services.AddDbContext<ELogBookDbContext>();
        // Register configuration
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