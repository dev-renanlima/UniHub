using UniHub.API.Mapper;
using UniHub.API.Middleware;
using UniHub.CrossCutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Serviços nativos da aplicação
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

// Configuração do AutoMapper
builder.Services.RegisterMaps();

// Middleware
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

// Injeções do projeto centralizadas
builder.Services.AddUniHubContext(builder.Configuration);
builder.Services.AddUniHubServices();
builder.Services.AddUniHubRepositories();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
