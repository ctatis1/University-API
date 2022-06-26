// 1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

//2. Connection with SQL server Express
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3. Add context to Services Builder
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();

//7. añadir el servicio de auth de JWT
//TODO: builder.Services.AddJwtTokenServices(builder.Configuration);


//4. Add Custom Services (folder Services)
builder.Services.AddScoped<IStudentsService, StudentsService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//8. TODO: configurar Swagger para que acepte la auth de JWT
builder.Services.AddSwaggerGen();


// 5. CORS Configuration
builder.Services.AddCors(options =>
{
    //con esto cualquiera podría utilizar la API para hacer consultas 
    options.AddPolicy(name: "CORSpolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});


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

//6. Tell the app to use the CORS
app.UseCors("CORSpolicy");

app.Run();
