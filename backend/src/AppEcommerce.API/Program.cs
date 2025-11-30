using AppEcommerce.Infra.Data.Context;
using AppEcommerce.Infra.Data.Repositories;
using System.Reflection;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Application.Interfaces;
using AppEcommerce.Application.Services;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;
using AppEcommerce.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("EcommercePolicy",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:3000",
                    "https://localhost:3000",
                    "https://shop-verde-dorado.lovable.app"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Registrar serviços do projeto de infra via reflexão (evita dependência de tipos concretos em tempo de compilação)
var infraAssembly = Assembly.Load(new AssemblyName("AppEcommerce.Infra.Data"));

var jsonContextType = infraAssembly.GetType("AppEcommerce.Infra.Data.Context.JsonContext");
if (jsonContextType != null)
    builder.Services.AddScoped(jsonContextType);

var clienteRepoType = infraAssembly.GetType("AppEcommerce.Infra.Data.Repositories.ClienteRepository");
var produtoRepoType = infraAssembly.GetType("AppEcommerce.Infra.Data.Repositories.ProdutoRepository");
var pagamentoRepoType = infraAssembly.GetType("AppEcommerce.Infra.Data.Repositories.PagamentoRepository");

if (clienteRepoType != null)
    builder.Services.AddScoped(typeof(AppEcommerce.Domain.Interfaces.IClienteRepository), clienteRepoType);
if (produtoRepoType != null)
    builder.Services.AddScoped(typeof(AppEcommerce.Domain.Interfaces.IProdutoRepository), produtoRepoType);
if (pagamentoRepoType != null)
    builder.Services.AddScoped(typeof(AppEcommerce.Domain.Interfaces.IPagamentoRepository), pagamentoRepoType);

builder.Services.AddScoped<AppEcommerce.Application.Services.ClienteService>();
builder.Services.AddScoped<AppEcommerce.Application.Services.ProdutoService>();
builder.Services.AddScoped<AppEcommerce.Application.Services.PagamentoService>();

builder.Services.AddSingleton<JsonContext>();
builder.Services.AddScoped<IItemCarrinhoRepository, ItemCarrinhoRepository>();
builder.Services.AddScoped<IItemCarrinhoService, ItemCarrinhoService>();


builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("EcommercePolicy");

app.MapControllers();

app.Run();
