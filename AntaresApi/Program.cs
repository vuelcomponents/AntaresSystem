using System.Text.Json.Serialization;
using AntaresApi;
using AntaresApi.Integrations.Employee;
using AntaresApi.Integrations.Offer;
using AntaresApi.Services;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IVariantService, VariantService>();
builder.Services.AddScoped<IVariantRealisationService, VariantRealisationService>();
builder.Services.AddScoped<IRealisationService, RealisationService>();
builder.Services.AddScoped<IVariantTypeService, VariantTypeService>();
builder.Services.AddScoped<I_DocumentService, _DocumentService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IStatusActionService, StatusActionService>();
builder.Services.AddScoped<IStatusActionTriggerService, StatusActionTriggerService>();
builder.Services.AddScoped<IActionFunctionService, ActionFunctionService>();
builder.Services.AddScoped<ISystemFunctionService, SystemFunctionService>();
builder.Services.AddScoped<IDocumentTypeService, _DocumentTypeService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IActionResolver, ActionResolver>();
builder.Services.AddScoped<IPositionService,PositionService>();
builder.Services.AddScoped<IPositionUnitService, PositionUnitService>();
builder.Services.AddScoped<IHouseService, HouseService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IRecruitmentService, RecruitmentService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IOfferIntegrationService, OfferIntegrationService>();
builder.Services.AddScoped<IEmployeeIntegrationService, EmployeeIntegrationService>();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;
});

// Automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var connectString = "Server=localhost;Port=1214;Database=antares;User=root;Password=Touxai88x@";
// builder.Services.AddDbContext<DataContext>(option =>
//     option.UseSqlServer(connectString));
builder.Services.AddDbContext<DataContext>(option =>
    option.UseMySql(connectString,    new MariaDbServerVersion(new Version(10, 5, 12)) ));

// "Connect":  "Server=localhost;Port=1214;Database=ymep;User=root;Password=Tempurio_22@TT&;"


builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");

if (File.Exists(envFilePath))
{
    var lines = File.ReadAllLines(envFilePath);
    foreach (var line in lines)
    {
        var parts = line.Split('=');
        if (parts.Length == 2)
        {
            var key = parts[0].Trim();
            var value = parts[1].Trim();
            Environment.SetEnvironmentVariable(key, value);
        }
    }

    Console.WriteLine("Loaded environment variables from .env file.");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

var app = builder.Build();
app.UseCors("AllowAll");
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