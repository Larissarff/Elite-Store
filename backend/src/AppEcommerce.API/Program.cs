using AppEcommerce.Infra.Data.Context;
using AppEcommerce.Infra.Data.Repositories;
using AppEcommerce.Domain.Interfaces;

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

builder.Services.AddScoped<JsonContext>(); // Registro do JsonContext

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("EcommercePolicy");

app.MapControllers();

app.Run();
