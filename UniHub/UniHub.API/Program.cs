using Asp.Versioning.ApiExplorer;
using System.Text.Json.Serialization;
using UniHub.API.Conventions;
using UniHub.API.Mapper;
using UniHub.API.Middleware;
using UniHub.CrossCutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Servi�os essenciais
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new DefaultProducesConvention());
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Options
builder.Services.AddUniHubOptions();

// Autentica��o e Autoriza��o
builder.Services.AddUniHubAuth();

// Vers�es da API e Swagger
builder.Services.AddUniHubApiVersioning();
builder.Services.AddUniHubSwagger();

// Logging
builder.Services.AddLogging();

// AutoMapper
builder.Services.RegisterMaps();

// Middleware
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

// Inje��es do projeto
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
app.UseSwaggerUI(options =>
{
    foreach (var desc in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"UniHub API {desc.GroupName.ToUpperInvariant()}");
    }

    options.RoutePrefix = "unihub/swagger";
});

// HTTPS
app.UseHttpsRedirection();

// CORS
app.UseCors("AllowAll");

// Middleware de Exce��es globais
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Autentica��o/Autoriza��o 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
