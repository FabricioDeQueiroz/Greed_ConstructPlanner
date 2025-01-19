using Construct_Planner.Components;
using Construct_Planner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

    // Adicionar serviços necessários para Blazor Server
    services.AddRazorComponents()
        .AddInteractiveServerComponents();
    
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
    
    // Adicionar serviços ao contêiner
    services.AddHttpClient("LocalAPI", client =>
    {
        client.BaseAddress = new Uri("http://localhost:5165/");
    });
}

void ConfigureMiddleware(WebApplication apiapp)
{
    // Usar CORS
    apiapp.UseCors("AllowAllOrigins");

    if (apiapp.Environment.IsDevelopment())
    {
        apiapp.UseSwagger();
        apiapp.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Construct Planner v1");
            c.InjectStylesheet("/css/SwaggerDark.css");
        });
    }

    // Configuração do pipeline de requisições
    apiapp.UseStaticFiles();
    apiapp.UseRouting();

    apiapp.UseAuthorization();
    apiapp.UseAntiforgery();
    
    apiapp.MapControllers();

    apiapp.MapRazorComponents<App>().AddInteractiveServerRenderMode();
}