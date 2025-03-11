using Azure.Identity;
using EmployeeManagement.App.Data;
using EmployeeManagement.App.Repository;
using EmployeeManagement.App.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = string.Empty;
if (!builder.Environment.IsDevelopment())
{
    // this is for keyvault
    var keyVaultUrl = new Uri(builder.Configuration.GetValue<string>("KeyVaultUrl")!);
    var defaultAzureCredential = new DefaultAzureCredential();
    builder.Configuration.AddAzureKeyVault(keyVaultUrl, defaultAzureCredential);
    connectionString = builder.Configuration.GetValue<string>("EmployeeDbConnection");

}
else
{
    // Configure Entity Framework Core with SQL Server. This is for local
    connectionString = builder.Configuration.GetConnectionString("EmployeeDbConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
    }    
}

builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(connectionString));

// Register employee repository and service dependencies
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Register the Contact repository and service
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error/500");
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Authentication must come before Authorization.
app.UseAuthorization();

// Configure status code pages so that errors like 404 are handled by our ErrorController.
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



