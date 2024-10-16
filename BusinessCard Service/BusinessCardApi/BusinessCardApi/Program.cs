using BusinessCardApi.AppDatabaseContext;
using BusinessCardApi.Repository;
using BusinessCardApi.Repository.ExportBusinessCardFileRepository;
using BusinessCardApi.Repository.ExportBusinessCardFileRespository;
using BusinessCardApi.Service;
using BusinessCardApi.Service.ExportBusinessCardFileService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders(); 
builder.Logging.AddConsole();  
builder.Logging.AddDebug();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBusinessCardRepository,BusinessCardRepository>();
builder.Services.AddScoped<IBusinessCardService,BusinessCardService>();
builder.Services.AddScoped<IFileUploadBusinessCardRepository,FileUploadBusinessCardRepository>();
builder.Services.AddScoped<IBusinessCardFacade,BusinessCardFacade>();
builder.Services.AddScoped<IExportFileRepository, ExportFileRepository>();
builder.Services.AddScoped<IExportFileService, ExportFileService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyMethod()                    
              .AllowAnyHeader()                   
              .AllowCredentials();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowLocalhost4200");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
