using System.Collections;
using System.IO.Compression;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/Ola", () =>
{
    string ola = "OLA ";
    return ola;
}
);

app.MapGet("/disciplina", () => 
{
    return new
    {
        Disciplina = "Desenvolvmento de Software Visual",
        Professor = "Marlon"
        
    };
    
}
);

app.MapGet("/hora", () =>
{
    DateTime data = DateTime.Now;
    return data;
}
);


List<Disciplina> disciplinas = new List<Disciplina>();

disciplinas.Add(new Disciplina
{
    id = 1,
    nome ="DSV",
    cargaHoraria = 80
});
disciplinas.Add(new Disciplina
{
    id = 2,
    nome = "BD",
    cargaHoraria = 120
});
disciplinas.Add(new Disciplina
{
    id = 3,
    nome = "Redes",
    cargaHoraria = 40
});
disciplinas.Add(new Disciplina
{
    id = 4,
    nome = "ES",
    cargaHoraria = 200
});    

app.MapGet("/disciplinas", () =>
{
    return disciplinas;
});

app.MapGet("/disciplinas/{id}", (int id) =>
{
    var disciplina = disciplinas.FirstOrDefault(d => d.id == id);
    if(disciplina == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(disciplina);

});

app.MapPost("/diciplinas", (Disciplina novadiciplina) =>
{
   disciplinas.Add(novadiciplina);
   return  Results.Ok(novadiciplina);
});


app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
