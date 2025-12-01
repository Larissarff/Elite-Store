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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Contexto JSON
builder.Services.AddScoped<JsonContext>();

// Repositórios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
builder.Services.AddScoped<IItemCarrinhoRepository, ItemCarrinhoRepository>();
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();

// Services (sempre interface + implementação)
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPagamentoService, PagamentoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IItemCarrinhoService, ItemCarrinhoService>();
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("EcommercePolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
