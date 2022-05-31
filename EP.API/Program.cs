using EP.BLL;
using EP.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "My Minimaal Apie",
        Description = "Music",
        Version = "v1"
    });
});

var app = builder.Build();
MusicManager mm = new();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimaal Apie");
});

app.MapGet("/", () => "Hello World!");
app.MapGet("/pieces", async () => await mm.GetAsync(0, 10));
app.MapPost("piece", async ([FromBody] Piece piece) =>
{
    piece = await mm.CreateAsync(piece);
    return Results.Created($"/piece/{piece.ID}", piece);
});


//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//}
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

app.Run();
