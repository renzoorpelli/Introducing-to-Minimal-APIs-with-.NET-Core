using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSqlServer<DespachoContext>(builder.Configuration.GetConnectionString("DbContext"));

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/", () => "Hola mundo");

app.MapGet("/dbconexion", ([FromServices] DespachoContext dbContext) =>
{
    return dbContext.Database.EnsureCreated() ?  Results.StatusCode(200) :   Results.StatusCode(404);
});

app.MapGet("/listar", async ([FromServices] DespachoContext dbContext) =>
{
    return await dbContext.Cervezas.Include(f => f.Fabricante).ToListAsync()
    is List<Cerveza> cerveza ? 
    Results.Ok(cerveza):
    Results.StatusCode(404);
});

app.MapGet("/listar/{id}", async ([FromServices] DespachoContext dbContext, int id) =>
{
    return await dbContext.Cervezas.Include(f => f.Fabricante).SingleAsync(c => c.Id == id)
    is Cerveza cerveza ?
    Results.Ok(cerveza) :
    Results.StatusCode(404);

});


app.MapPost("/add", async ([FromServices] DespachoContext dbContext, Cerveza cervezaPost) => {

    dbContext.Cervezas.Add(cervezaPost);
    await dbContext.SaveChangesAsync();

    return cervezaPost is not null ?
    Results.Created($"/listar/{cervezaPost.Id}", cervezaPost)
    :Results.StatusCode(404);
});

app.MapPut("/update/{id}", async ([FromServices] DespachoContext dbContext, Cerveza cervezaPost ,int id) => {
    var cerveza = await dbContext.Cervezas.Include(f => f.Fabricante).SingleAsync(c => c.Id == id);

    if (cerveza is not null)
    {
        cerveza.Nombre = cervezaPost.Nombre;
        cerveza.Descripcion = cervezaPost.Descripcion;
        cerveza.FabricanteId = cervezaPost.FabricanteId;
        cerveza.Fabricante = cervezaPost.Fabricante;

        await dbContext.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.MapDelete("/remove/{id}", async ([FromServices] DespachoContext dbContext, int id) => {
    var cerveza = await dbContext.Cervezas.Include(f => f.Fabricante).SingleAsync(c => c.Id == id);
    if(cerveza is not null)
    {
        dbContext.Cervezas.Remove(cerveza);
        await dbContext.SaveChangesAsync();
        return Results.StatusCode(200);
    }
    return Results.NotFound();
});
app.Run();

