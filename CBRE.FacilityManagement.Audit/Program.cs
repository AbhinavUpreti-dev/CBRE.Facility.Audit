using CBRE.FacilityManagement.Audit.Application.Contracts.Persistence;
using CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetCustomers;
using CBRE.FacilityManagement.Audit.Application.MappingProfile;
using CBRE.FacilityManagement.Audit.Persistence.Repository;
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