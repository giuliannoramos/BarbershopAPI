using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Customers.Commands.CreateCustomer;
using Barbearia.Application.Features.Customers.Commands.DeleteCustomer;
using Barbearia.Application.Features.Customers.Commands.UpdateCustomer;
using Barbearia.Application.Features.Customers.Queries.GetAllCustomers;
using Barbearia.Application.Features.Customers.Queries.GetCustomerById;
using Barbearia.Persistence.DbContexts;
using Barbearia.Persistence.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//config porta padrão
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//config automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

//config mediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddTransient<IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdDto>, GetCustomerByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllCustomersQuery, IEnumerable<GetAllCustomersDto>>, GetAllCustomersQueryHandler>();

builder.Services.AddTransient<IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>, CreateCustomerCommandHandler>();
builder.Services.AddTransient<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();

builder.Services.AddTransient<IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>, UpdateCustomerCommandHandler>();
builder.Services.AddTransient<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();

builder.Services.AddTransient<IRequestHandler<DeleteCustomerCommand, bool>, DeleteCustomerCommandHandler>();

//config banco de dados
builder.Services.AddDbContext<CustomerContext>(options =>
{
    options.UseNpgsql("Host=localhost;port=5432;Database=Barbearia;Username=postgres;Password=1973");
}
);

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

using (var scope = app.Services.CreateScope())
{
    try
    {
        var customerContext = scope.ServiceProvider.GetService<CustomerContext>();

        if (customerContext != null)
        {
            await customerContext.Database.EnsureDeletedAsync();
            await customerContext.Database.MigrateAsync();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocorreu um erro ao atualizar o banco de dados: {ex.Message}");
    }
}

app.Run();