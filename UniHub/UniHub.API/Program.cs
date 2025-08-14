using UniHub.API.Conventions;
using UniHub.API.Mapper;
using UniHub.API.Middleware;
using UniHub.CrossCutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Serviços essenciais
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new DefaultProducesConvention());
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Versões da API e Swagger
builder.Services.AddUniHubApiVersioning();
builder.Services.AddUniHubSwagger();

// Logging
builder.Services.AddLogging();

// AutoMapper
builder.Services.RegisterMaps();

// Middleware
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

// Injeções do projeto
builder.Services.AddUniHubContext(builder.Configuration);
builder.Services.AddUniHubServices();
builder.Services.AddUniHubRepositories();

// CORS
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

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniHub API v1");
});

// Middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

// Ativa o CORS
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();