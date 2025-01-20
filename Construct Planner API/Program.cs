using Construct_Planner_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// Configuração de Serviços
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configuração do Middleware
ConfigureMiddleware(app);

app.Run();
return;

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Configuração do banco de dados - PostgreSQL
    services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    
    services.AddControllers();
    services.AddEndpointsApiExplorer();

    // Configurar Swagger para incluir suporte a header de token
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Construct Planner", Version = "v1" });
    });
    
    // Configurar CORS para permitir todos os métodos e cabeçalhos
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}

void ConfigureMiddleware(WebApplication apiapp)
{
    // Usar CORS
    apiapp.UseCors("AllowAllOrigins");
    
    apiapp.UseSwagger();
    apiapp.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Construct Planner v1");
        c.InjectStylesheet("/swagger-css/SwaggerDark.css");
    });
    apiapp.UseStaticFiles();
    
    apiapp.UseAuthorization();
    apiapp.MapControllers();
}