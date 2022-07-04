using EmployeeTaskSolicy.Context;
using EmployeeTaskSolicy.CreateDB;
using EmployeeTaskSolicy.Repository;
using EmployeeTaskSolicy.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSingleton<EmployeeContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("api/Employee/Error");
}

app.UseAuthorization();

app.MapControllers();

//Creating Db if it is not existing
CreateDB.Create(new EmployeeContext(builder.Configuration));

app.Run();
