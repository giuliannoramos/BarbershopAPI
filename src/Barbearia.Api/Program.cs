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
using Barbearia.Application.Features.Payments.Queries.GetPayment;
using Barbearia.Application.Features.Payments.Commands.CreatePayment;
using Barbearia.Application.Features.Payments.Commands.UpdatePayment;
using Barbearia.Application.Features.Payments.Commands.DeletePayment;
using Barbearia.Application.Features.ProductsCollection;
using Barbearia.Application.Features.Suppliers.Queries.GetSupplierById;
using Barbearia.Application.Features.Suppliers.Commands.CreateSupplier;
using Barbearia.Application.Features.Suppliers.Commands.UpdateSupplier;
using Barbearia.Application.Features.Suppliers.Commands.DeleteSupplier;
using Barbearia.Application.Features.SuppliersCollection;
using Barbearia.Application.Features.Products.Queries.GetAllProducts;
using Barbearia.Application.Features.Products.Queries.GetProductById;
using Barbearia.Application.Features.Products.Commands.CreateProduct;
using Barbearia.Application.Features.Products.Commands.DeleteProduct;
using Barbearia.Application.Features.Products.Commands.UpdateProduct;
using Barbearia.Application.Features.Employees.Queries.GetEmployeeById;
using Barbearia.Application.Features.Employees.Commands.CreateEmployee;
using Barbearia.Application.Features.Employees.Commands.UpdateEmployee;
using Barbearia.Application.Features.Employees.Commands.DeleteEmployee;
using Barbearia.Application.Features.EmployeesCollection;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Application.Features.Coupons.Commands.DeleteCoupon;
using Barbearia.Application.Features.StockHistories.Queries.GetStockHistoryById;
using Barbearia.Application.Features.StockHistories.Commands.CreateStockHistory;
using Barbearia.Application.Features.StockHistories.Commands.UpdateStockHistory;
using Barbearia.Application.Features.StockHistories.Commands.DeleteStockHistory;
using Barbearia.Application.Features.Schedules.Commands.CreateSchedule;
using Barbearia.Application.Features.Schedules.Queries.GetScheduleById;
using Barbearia.Application.Features.SchedulesCollection;
using Barbearia.Application.Features.Schedules.Queries.GetAllSchedules;
using Barbearia.Application.Features.Schedules.Commands.DeleteSchedule;
using Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;
using Barbearia.Application.Features.ProductCategories.Queries.GetAllProductCategories;
using Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;
using Barbearia.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Barbearia.Application.Features.ProductCategories.Queries.GetProductCategoryById;
using Barbearia.Application.Features.Services.Queries.GetServiceById;
using Barbearia.Application.Features.Services.Commands.UpdateService;
using Barbearia.Application.Features.Services.Commands.CreateService;
using Barbearia.Application.Features.Services.Commands.DeleteService;
using Barbearia.Application.Features.ServicesCollection;

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

builder.Logging.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);


//config automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

//config mediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
// Customer commands and queries
builder.Services.AddScoped<IRequestHandler<GetCustomersCollectionQuery, GetCustomersCollectionQueryResponse>, GetCustomersCollectionQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdDto>, GetCustomerByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllCustomersQuery, IEnumerable<GetAllCustomersDto>>, GetAllCustomersQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetCustomerWithOrdersByIdQuery, GetCustomerWithOrdersByIdDto>, GetCustomerWithOrdersByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>, CreateCustomerCommandHandler>();
builder.Services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();
builder.Services.AddScoped<IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>, UpdateCustomerCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();
builder.Services.AddScoped<IRequestHandler<DeleteCustomerCommand, bool>, DeleteCustomerCommandHandler>();
// Supplier commands and queries
builder.Services.AddScoped<IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdDto>, GetSupplierByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateSupplierCommand, CreateSupplierCommandResponse>, CreateSupplierCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateSupplierCommand, UpdateSupplierCommandResponse>, UpdateSupplierCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteSupplierCommand, bool>, DeleteSupplierCommandHandler>();
// Address commands and queries
builder.Services.AddScoped<IRequestHandler<GetAddressQuery, IEnumerable<GetAddressDto>>, GetAddressQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>, CreateAddressCommandHandler>();
builder.Services.AddScoped<IValidator<CreateAddressCommand>, CreateAddressCommandValidator>();
builder.Services.AddScoped<IRequestHandler<UpdateAddressCommand, UpdateAddressCommandResponse>, UpdateAddressCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateAddressCommand>, UpdateAddressCommandValidator>();
builder.Services.AddScoped<IRequestHandler<DeleteAddressCommand, bool>, DeleteAddressCommandHandler>();
// Telephone commands and queries
builder.Services.AddScoped<IRequestHandler<GetTelephoneQuery, IEnumerable<GetTelephoneDto>>, GetTelephoneQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateTelephoneCommand, CreateTelephoneCommandResponse>, CreateTelephoneCommandHandler>();
builder.Services.AddScoped<IValidator<CreateTelephoneCommand>, CreateTelephoneCommandValidator>();
builder.Services.AddScoped<IRequestHandler<UpdateTelephoneCommand, UpdateTelephoneCommandResponse>, UpdateTelephoneCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateTelephoneCommand>, UpdateTelephoneCommandValidator>();
builder.Services.AddScoped<IRequestHandler<DeleteTelephoneCommand, bool>, DeleteTelephoneCommandHandler>();
// Order commands and queries
builder.Services.AddScoped<IRequestHandler<GetOrdersCollectionQuery, GetOrdersCollectionQueryResponse>, GetOrdersCollectionQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllOrdersQuery, IEnumerable<GetAllOrdersDto>>, GetAllOrdersQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetOrderByIdQuery, GetOrderByIdDto>, GetOrderByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetOrderByNumberQuery, GetOrderByNumberDto>, GetOrderByNumberQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
builder.Services.AddScoped<IRequestHandler<UpdateOrderCommand, UpdateOrderCommandResponse>, UpdateOrderCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateOrderCommand>, UpdateOrderCommandValidator>();
builder.Services.AddScoped<IRequestHandler<DeleteOrderCommand, bool>, DeleteOrderCommandHandler>();
// Payment commands and queries
builder.Services.AddScoped<IRequestHandler<GetSuppliersCollectionQuery, GetSuppliersCollectionQueryResponse>, GetSuppliersCollectionQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetPaymentQuery, GetPaymentDto>, GetPaymentQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreatePaymentCommand, CreatePaymentCommandResponse>, CreatePaymentCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdatePaymentCommand, UpdatePaymentCommandResponse>, UpdatePaymentCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeletePaymentCommand, bool>, DeletePaymentCommandHandler>();
// Product commands and queries
builder.Services.AddScoped<IRequestHandler<GetProductsCollectionQuery, GetProductsCollectionQueryResponse>, GetProductsCollectionQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsDto>>, GetAllProductsQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetProductByIdQuery, GetProductByIdDto>, GetProductByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateProductCommand, CreateProductCommandResponse>, CreateProductCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteProductCommand, bool>, DeleteProductCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>, UpdateProductCommandHandler>();
// Employee commands and queries
builder.Services.AddScoped<IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdDto>, GetEmployeeByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateEmployeeCommand, CreateEmployeeCommandResponse>, CreateEmployeeCommandHandler>();
builder.Services.AddScoped<IValidator<CreateEmployeeCommand>, CreateEmployeeCommandValidator>();
builder.Services.AddScoped<IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResponse>, UpdateEmployeeCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateEmployeeCommand>, UpdateEmployeeCommandValidator>();
builder.Services.AddScoped<IRequestHandler<DeleteEmployeeCommand, bool>, DeleteEmployeeCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetEmployeesCollectionQuery, GetEmployeesCollectionQueryResponse>, GetEmployeesCollectionQueryHandler>();
// Coupon commands and queries
builder.Services.AddScoped<IRequestHandler<GetCouponByIdQuery, GetCouponByIdDto>, GetCouponByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllCouponsQuery,IEnumerable<GetAllCouponsDto>>,GetAllCouponsQueryHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateCouponCommand,UpdateCouponCommandResponse>,UpdateCouponCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateCouponCommand, CreateCouponCommandResponse>, CreateCouponCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteCouponCommand, bool>, DeleteCouponCommandHandler>();

builder.Services.AddScoped<IRequestHandler<GetStockHistoryByIdQuery, GetStockHistoryByIdDto>, GetStockHistoryByIdQueryHandler>();
// builder.Services.AddScoped<IRequestHandler<GetAllStockHistoriesQuery, IEnumerable<GetAllStockHistoriesDto>>, GetAllStockHistoriesQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateStockHistoryCommand, CreateStockHistoryCommandResponse>, CreateStockHistoryCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateStockHistoryCommand, UpdateStockHistoryCommandResponse>, UpdateStockHistoryCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteStockHistoryCommand, bool>, DeleteStockHistoryCommandHandler>();
// Schedule commands and queries
builder.Services.AddScoped<IRequestHandler<GetSchedulesCollectionQuery, GetSchedulesCollectionQueryResponse>, GetSchedulesCollectionQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllSchedulesQuery,IEnumerable<GetAllSchedulesDto>>, GetAllSchedulesQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetScheduleByIdQuery, GetScheduleByIdDto>, GetScheduleByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateScheduleCommand,CreateScheduleCommandResponse>,CreateScheduleCommandHandler>();
builder.Services.AddScoped<IValidator<CreateScheduleCommand>,CreateScheduleCommandValidator>();
builder.Services.AddScoped<IRequestHandler<DeleteScheduleCommand, bool>, DeleteScheduleCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateScheduleCommand, UpdateScheduleCommandResponse>, UpdateScheduleCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateScheduleCommand>, UpdateScheduleCommandValidator>();
// Product Category commands and queries
builder.Services.AddScoped<IRequestHandler<GetAllProductCategoriesQuery,IEnumerable<GetAllProductCategoriesDto>>, GetAllProductCategoriesQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetProductCategoryByIdQuery, GetProductCategoryByIdDto>, GetProductCategoryByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateProductCategoryCommand,CreateProductCategoryCommandResponse>,CreateProductCategoryCommandHandler>();
builder.Services.AddScoped<IValidator<CreateProductCategoryCommand>,CreateProductCategoryCommandValidator>();
builder.Services.AddScoped<IRequestHandler<DeleteProductCategoryCommand, bool>, DeleteProductCategoryCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateProductCategoryCommand, UpdateProductCategoryCommandResponse>, UpdateProductCategoryCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateProductCategoryCommand>, UpdateProductCategoryCommandValidator>();
// Service commands and queries
builder.Services.AddScoped<IRequestHandler<GetServiceByIdQuery, GetServiceByIdDto>, GetServiceByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateServiceCommand,UpdateServiceCommandResponse>,UpdateServiceCommandHandler>();
builder.Services.AddScoped<IValidator<UpdateServiceCommand>, UpdateServiceCommandValidator>();
builder.Services.AddScoped<IRequestHandler<CreateServiceCommand, CreateServiceCommandResponse>, CreateServiceCommandHandler>();
builder.Services.AddScoped<IValidator<CreateServiceCommand>, CreateServiceCommandValidator>();
builder.Services.AddScoped<IRequestHandler<DeleteServiceCommand, bool>, DeleteServiceCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetServicesCollectionQuery, GetServicesCollectionQueryResponse>, GetServicesCollectionQueryHandler>();

//config banco de dados
builder.Services.AddDbContext<PersonContext>(options =>
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
    options.UseNpgsql("Host=localhost;port=5432;Database=Barbearia;Username=postgres;Password=1973;Include Error Detail=true");
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
        var customerContext = scope.ServiceProvider.GetService<PersonContext>();
        var orderContext = scope.ServiceProvider.GetService<OrderContext>();
        var itemContext = scope.ServiceProvider.GetService<ItemContext>();

        if (customerContext != null)
        {
            await customerContext.Database.EnsureDeletedAsync();
            await customerContext.Database.MigrateAsync();
        }
        if (orderContext != null)
        {
            await orderContext.Database.MigrateAsync();
        }
        if (itemContext != null)
        {
            await itemContext.Database.MigrateAsync();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocorreu um erro ao atualizar o banco de dados: {ex.Message}");
    }
}

app.Run();