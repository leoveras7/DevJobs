using DEVJOBS.API.Persistence;
using DEVJOBS.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DevJobsContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DevJobsCs")));
builder.Services.AddScoped<IJobVacancyRepository, JobVacancyRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//    c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "DevJobs.API",
//        Version = "v1",
//        Contact = new OpenApiContact
//        {
//            Name = "LuisDev",
//            Email = "contato@luisdev.com.br",
//            Url = new Uri("https://luisdev.com.br")
//        }

//    });

//    var xmlFile = "DevJobs.API.xml";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    c.IncludeXmlComments(xmlPath);

//});


var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
