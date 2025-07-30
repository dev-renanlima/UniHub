using Asp.Versioning;
using UniHub.API.Conventions;
using UniHub.API.Mapper;
using UniHub.API.Middleware;
using UniHub.CrossCutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Vers�o API
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Servi�os
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new DefaultProducesConvention());
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

// AutoMapper
builder.Services.RegisterMaps();

// Middleware
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

// Inje��es do projeto
builder.Services.AddUniHubContext(builder.Configuration);
builder.Services.AddUniHubServices();
builder.Services.AddUniHubRepositories();

// CORS liberado
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// Ativa o CORS
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();
