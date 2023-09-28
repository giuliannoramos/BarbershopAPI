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
using Barbearia.Application.Features.Customers.Queries.GetCustomerWithOrdersById;
using Barbearia.Persistence.DbContexts;
using Barbearia.Persistence.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Barbearia.Application.Features.Orders.Commands.UpdateOrder;
using Barbearia.Application.Features.Orders.Commands.DeleteOrder;
using Barbearia.Application.Features.CustomersCollection;
using Microsoft.Extensions.Options;
using Elmah.Io.Extensions.Logging;
using Barbearia.Application.Features.OrdersCollection;
using Barbearia.Application.Features.Orders.Queries.GetAllOrders;
using Barbearia.Application.Features.Orders.Queries.GetOrderById;
using Barbearia.Application.Features.Orders.Queries.GetOrderByNumber;
using Barbearia.Application.Features.Orders.Commands.CreateOrder;

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

builder.Logging.AddElmahIo(options =>
{
    options.ApiKey = "d8c818e18e154af08ffa697802b4687a";
    options.LogId = new Guid("92f71801-82ae-469e-b6ed-e4c9cc6cb221");
});

builder.Logging.AddFilter<ElmahIoLoggerProvider>(null,LogLevel.Warning);

//config automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//config mediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddScoped<IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdDto>, GetCustomerByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllCustomersQuery, IEnumerable<GetAllCustomersDto>>, GetAllCustomersQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetCustomerWithOrdersByIdQuery, GetCustomerWithOrdersByIdDto>, GetCustomerWithOrdersByIdQueryHandler>();


builder.Services.AddScoped<IRequestHandler<GetCustomersCollectionQuery, GetCustomersCollectionQueryResponse>, GetCustomersCollectionQueryHandler>();


builder.Services.AddScoped<IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>, CreateCustomerCommandHandler>();
builder.Services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();

builder.Services.AddScoped<IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>, UpdateCustomerCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();

builder.Services.AddScoped<IRequestHandler<DeleteCustomerCommand, bool>, DeleteCustomerCommandHandler>();

builder.Services.AddScoped<IRequestHandler<GetAddressQuery, IEnumerable<GetAddressDto>>, GetAddressQueryHandler>();

builder.Services.AddScoped<IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>, CreateAddressCommandHandler>();
builder.Services.AddScoped<IValidator<CreateAddressCommand>, CreateAddressCommandValidator>();

builder.Services.AddScoped<IRequestHandler<UpdateAddressCommand, UpdateAddressCommandResponse>, UpdateAddressCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateAddressCommand>, UpdateAddressCommandValidator>();

builder.Services.AddScoped<IRequestHandler<DeleteAddressCommand, bool>, DeleteAddressCommandHandler>();

builder.Services.AddScoped<IRequestHandler<GetTelephoneQuery, IEnumerable<GetTelephoneDto>>, GetTelephoneQueryHandler>();

builder.Services.AddScoped<IRequestHandler<CreateTelephoneCommand, CreateTelephoneCommandResponse>, CreateTelephoneCommandHandler>();
builder.Services.AddScoped<IValidator<CreateTelephoneCommand>, CreateTelephoneCommandValidator>();

builder.Services.AddScoped<IRequestHandler<UpdateTelephoneCommand, UpdateTelephoneCommandResponse>, UpdateTelephoneCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateTelephoneCommand>, UpdateTelephoneCommandValidator>();

builder.Services.AddScoped<IRequestHandler<GetOrdersCollectionQuery, GetOrdersCollectionQueryResponse>, GetOrdersCollectionQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllOrdersQuery, IEnumerable<GetAllOrdersDto>>, GetAllOrdersQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetOrderByIdQuery, GetOrderByIdDto>, GetOrderByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetOrderByNumberQuery, GetOrderByNumberDto>, GetOrderByNumberQueryHandler>();

builder.Services.AddScoped<IRequestHandler<DeleteTelephoneCommand, bool>, DeleteTelephoneCommandHandler>();

builder.Services.AddScoped<IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();

builder.Services.AddScoped<IRequestHandler<UpdateOrderCommand, UpdateOrderCommandResponse>, UpdateOrderCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateOrderCommand>, UpdateOrderCommandValidator>();

builder.Services.AddScoped<IRequestHandler<DeleteOrderCommand, bool>, DeleteOrderCommandHandler>();



//config banco de dados
builder.Services.AddDbContext<CustomerContext>(options =>
{
    options.UseNpgsql("Host=localhost;port=5432;Database=Barbearia;Username=postgres;Password=1973");
}
);

builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseNpgsql("Host=localhost;port=5432;Database=Barbearia;Username=postgres;Password=1973");
}
);

builder.Services.AddDbContext<ItemContext>(options =>
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

app.UseElmahIoExtensionsLogging();

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