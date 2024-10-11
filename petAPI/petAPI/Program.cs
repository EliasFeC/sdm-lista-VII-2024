using Microsoft.EntityFrameworkCore;
using petAPI.Data;
using petAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("animaldb"));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapGet("/animais", async(AppDbContext db) => await db.Animal.ToListAsync());

app.MapPost("/animais", async (AppDbContext db, Animal animal) =>
{
  await db.Animal.AddAsync(animal);
  await db.SaveChangesAsync();
  return Results.Created($"/animal/{animal.Id}", animal);
});
app.MapDelete("/animal/{id:int}", async (int id, AppDbContext db) =>
{
  var animal = await db.Animal.FindAsync(id);
  if (animal == null) {
    return Results.NotFound();
  }
  db.Animal.Remove(animal);
  await db.SaveChangesAsync();

  return Results.NoContent();
});
app.MapPut("/animal/{id}", async (AppDbContext
db, Animal updateanimal, int id) =>
{
var animal = await db.Animal.FindAsync(id);
if (animal is null) return
Results.NotFound();
animal.Nome = updateanimal.Nome;
animal.Idade = updateanimal.Idade;
animal.Cor = updateanimal.Cor;
animal.Tipo = updateanimal.Tipo;
animal.Peso = updateanimal.Peso;
animal.Vacinado = updateanimal.Vacinado;
await db.SaveChangesAsync();
return Results.NoContent();
});
app.Run();
