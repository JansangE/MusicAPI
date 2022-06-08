using EP.BLL;
using EP.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Type = EP.DOMAIN.Type;

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
PieceManager pieceManager = new();
ComposerManager composerManager = new();
ArtistManager artistManager = new();
TypeManager typeManager = new();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimaal Apie");
});

app.MapGet("/", () => "Hello World!");


//CRUD Composer
app.MapGet("/countComposer", async () => await composerManager.GetTotalCountAsync());
app.MapGet("/composers", async () => await composerManager.GetAsync(0, 100));
app.MapGet("/composer", async (int id) => await composerManager.GetByIdAsync(id));
app.MapPost("/composer", async ([FromBody] Composer composer) =>
{
    composer = await composerManager.CreateAsync(composer);
    return Results.Created($"/composer/{composer.ID}", composer);
});
app.MapPut("/composer", async (int id, [FromBody] Composer composer) =>
{
    composer = await composerManager.UpdateAsync(id, composer);
    return Results.Created($"/composer/{composer.ID}", composer);
});
app.MapDelete("/composer", async (int id) => await composerManager.DeleteAsync(id));


//CRUD Artist
app.MapGet("/countArtist", async () => await artistManager.GetTotalCountAsync());
app.MapGet("/artists", async () => await artistManager.GetAsync(0, 100));
app.MapGet("/artist", async (int id) => await artistManager.GetByIdAsync(id));
app.MapPost("/artist", async ([FromBody] Artist artist) =>
{
    artist = await artistManager.CreateAsync(artist);
    return Results.Created($"artist/{artist.ID}", artist);
});
app.MapPut("/artist", async (int id, [FromBody] Artist artist) =>
{
    artist = await artistManager.UpdateAsync(id, artist);
    return Results.Created($"/artist/{artist.ID}", artist);
});
app.MapDelete("/artist", async (int id) => await artistManager.DeleteAsync(id));

//CRUD Type
app.MapGet("/countType", async () => await typeManager.GetTotalCountAsync());
app.MapGet("/types", async () => await typeManager.GetAsync(0, 100));
app.MapGet("/type", async (int id) => await typeManager.GetByIdAsync(id));
app.MapPost("/type", async ([FromBody] Type type) =>
{
    type = await typeManager.CreateAsync(type);
    return Results.Created($"type/{type.ID}", type);
});
app.MapPut("/type", async (int id, [FromBody] Type type) =>
{
    type = await typeManager.UpdateAsync(id, type);
    return Results.Created($"/type/{type.ID}", type);
});
app.MapDelete("/type", async (int id) => await typeManager.DeleteAsync(id));

//CRUD Piece
app.MapGet("/countPiece", async () => await pieceManager.GetTotalCountAsync());
app.MapGet("/pieces", async () => await pieceManager.GetAsync(0, 100));
app.MapGet("/piece", async (int id) => await pieceManager.GetByIdAsync(id));
app.MapPost("piece", async ([FromBody] Piece piece) =>
{
    piece = await pieceManager.CreateAsync(piece);
    return Results.Created($"/piece/{piece.ID}", piece);
});
app.MapPut("/piece", async (int id, [FromBody] Piece piece) =>
{
    piece = await pieceManager.UpdateAsync(id, piece);
    return Results.Created($"/piece/{piece.ID}", piece);
});
app.MapDelete("/piece", async (int id) => await pieceManager.DeleteAsync(id));


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
