using System.Text.Json.Serialization;
using MarsRover.Domain.Entities;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();
app
    .UseDefaultFiles()
    .UseStaticFiles();

app.MapPost("/upload", (HttpRequest request) =>
    {
        var files = request.Form.Files;
        //With more time i will use dependency injection instead of newing up objects
        var navigator = new Navigator();
        try
        {
            if (files[0].Length > 1024 * 1024*5)
            {
                return Results.BadRequest("File is too large.");
            }
            
            var result = navigator.GetNavigations(files[0].OpenReadStream());
            
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    })
    .Produces(200);

app.Run();