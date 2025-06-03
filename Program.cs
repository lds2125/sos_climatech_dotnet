using GsDotNet.Data;
using GsDotNet.Repositories;
using GsDotNet.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Cadastro de Pessoas Afetadas por Eventos Climáticos",
        Version = "v1",
        Description = "API para gerenciamento de eventos climáticos extremos e pessoas afetadas",
        Contact = new OpenApiContact
        {
            Name = "Equipe de Desenvolvimento",
            Email = "contato@exemplo.com"
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddScoped<IEventoClimaticoRepository, EventoClimaticoRepository>();
builder.Services.AddScoped<IPessoaAfetadaRepository, PessoaAfetadaRepository>();
builder.Services.AddScoped<IEventoClimaticoService, EventoClimaticoService>();
builder.Services.AddScoped<IPessoaAfetadaService, PessoaAfetadaService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Eventos Climáticos v1");
        c.RoutePrefix = "swagger";
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        c.DefaultModelsExpandDepth(-1); 
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao aplicar migrations: {ex.Message}");
    }
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
