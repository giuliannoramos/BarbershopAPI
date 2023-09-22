using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Addresses.Commands.CreateAddress;
using Barbearia.Application.Features.Addresses.Commands.DeleteAddress;
using Barbearia.Application.Features.Addresses.Commands.UpdateAddress;
using Barbearia.Application.Features.Addresses.Queries.GetAddress;
using Barbearia.Application.Features.Telephones.Commands.CreateTelephone;
using Barbearia.Application.Features.Telephones.Commands.DeleteTelephone;
using Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;
using Barbearia.Application.Features.Telephones.Query.GetTelephone;
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

builder.Services.AddTransient<IRequestHandler<GetAddressQuery, IEnumerable<GetAddressDto>>, GetAddressQueryHandler>();

builder.Services.AddTransient<IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>, CreateAddressCommandHandler>();
builder.Services.AddTransient<IValidator<CreateAddressCommand>, CreateAddressCommandValidator>();

builder.Services.AddTransient<IRequestHandler<UpdateAddressCommand, UpdateAddressCommandResponse>, UpdateAddressCommandHandler>();
builder.Services.AddTransient<IValidator<UpdateAddressCommand>, UpdateAddressCommandValidator>();

builder.Services.AddTransient<IRequestHandler<DeleteAddressCommand, bool>, DeleteAddressCommandHandler>();


builder.Services.AddTransient<IRequestHandler<GetTelephoneQuery, IEnumerable<GetTelephoneDto>>, GetTelephoneQueryHandler>();

builder.Services.AddTransient<IRequestHandler<CreateTelephoneCommand, CreateTelephoneCommandResponse>, CreateTelephoneCommandHandler>();
builder.Services.AddTransient<IValidator<CreateTelephoneCommand>, CreateTelephoneCommandValidator>();

builder.Services.AddTransient<IRequestHandler<UpdateTelephoneCommand, UpdateTelephoneCommandResponse>, UpdateTelephoneCommandHandler>();
builder.Services.AddTransient<IValidator<UpdateTelephoneCommand>, UpdateTelephoneCommandValidator>();

builder.Services.AddTransient<IRequestHandler<DeleteTelephoneCommand, bool>, DeleteTelephoneCommandHandler>();


//config banco de dados
builder.Services.AddDbContext<CustomerContext>(options =>
{
    options.UseNpgsql("Host=localhost;port=5432;Database=Barbearia;Username=postgres;Password=123456");
}
);

builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseNpgsql("Host=localhost;port=5432;Database=Barbearia;Username=postgres;Password=123456");
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
        var orderContext = scope.ServiceProvider.GetService<OrderContext>();

        if (customerContext != null)
        {
            await customerContext.Database.EnsureDeletedAsync();
            await customerContext.Database.MigrateAsync();
        }
        if (orderContext != null)
        {
            await orderContext.Database.MigrateAsync();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocorreu um erro ao atualizar o banco de dados: {ex.Message}");
    }
}

app.Run();