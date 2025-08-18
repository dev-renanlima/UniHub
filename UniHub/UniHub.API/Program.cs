using Asp.Versioning.ApiExplorer;
using System.Text.Json.Serialization;
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
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Autenticação e Autorização
builder.Services.AddUniHubAuthentication(builder.Configuration);

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

// Swagger
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    foreach (var desc in provider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"UniHub API {desc.GroupName.ToUpperInvariant()}");
    }
});

// Middleware Exceções globais
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Middleware API Key
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

// Ativa o CORS
app.UseCors("AllowAll");

// Auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();