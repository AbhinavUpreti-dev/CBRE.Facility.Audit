using CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetCustomers;
using CBRE.FacilityManagement.Audit.Application.MappingProfile;
using CBRE.FacilityManagement.Audit.Core.Harbour;
using CBRE.FacilityManagement.Audit.Persistence.CosmosDbRespository;
using CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts;
using CBRE.FacilityManagement.Audit.Persistence.Repository;
using CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
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

        var cosmosSettings = builder.Configuration.GetSection("Cosmos").Get<CosmosSettings>();

        var jsonSettings = new JsonSerializerSettings();

        // Serializer: By default, Cosmos SDK supports System.Text.Json but we can use CosmosJsonDotNetSerializer as below to support Newtonsoft.Json
        var options = new CosmosClientOptions()
        {
            ConnectionMode = Debugger.IsAttached ? ConnectionMode.Gateway : ConnectionMode.Direct,
            ConsistencyLevel = ConsistencyLevel.Session, // Depends on the Consistency level setup for the Cosmos DB service
            AllowBulkExecution = true, // Use 'false' when using Prod read-only connection string
            MaxRetryWaitTimeOnRateLimitedRequests = new TimeSpan(0, 0, 60),
            MaxRetryAttemptsOnRateLimitedRequests = 10,
            Serializer = new CosmosJsonDotNetSerializer(jsonSettings),
        };

        var cosmosClient = new CosmosClient(
               cosmosSettings.ServiceEndpoint,
               cosmosSettings.CosmosDbKey, // can be placed in keyvault and fetch it from secrets too !!
        options);

        builder.Services.AddSingleton(cosmosClient);
        builder.Services.AddSingleton<IRepository, HarbourCosmosDbRepository>();

        // Creating the database if it doesnt exists
        Microsoft.Azure.Cosmos.Database database = cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosSettings.DatabaseName).GetAwaiter().GetResult();

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