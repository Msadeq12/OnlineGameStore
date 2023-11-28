using EventService.Models;
using EventService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EventServiceContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("EventServiceContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*
 * All the HTTPS verb requests are down below usng the MapGet, MapPost, MapPut, MapDelete, and MapPatch methods.
 * 
 */

var events = app.MapGroup("/events");

//gets all the events
events.MapGet("/", async (EventServiceContext eventDB) =>
    await eventDB.Events.ToListAsync()
);

//gets event by id
events.MapGet("/{id}", async (int id, EventServiceContext eventDB) =>
{
    Event? eve = await eventDB.Events.FindAsync(id);

    if(eve == null)
    {
        return Results.BadRequest();
    }

    return Results.Ok(eve);

}
);

// adds event by POST
events.MapPost("/", async (Event eve, EventServiceContext eventDB) =>
{
    /*Event newEvent = new Event()
    {
        eventID = eve.Id,
        Name = eve.Name,
        Location = eve.Location,
        Description = eve.Description,
        StartDate = DateTime.Parse(eve.StartDate),
        EndDate = DateTime.Parse(eve.EndDate)

    };*/

    await eventDB.Events.AddAsync(eve);
    await eventDB.SaveChangesAsync();

    return Results.Created<Event>($"/events/{eve.eventID}", eve);
});

// deletes event by id
events.MapDelete("/{id}", async (int id, EventServiceContext eventDB) =>
{
    if(await eventDB.Events.FindAsync(id) is Event eventById)
    {
        eventDB.Events.Remove(eventById);
        await eventDB.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// updates event by id
events.MapPut("/{id}", async (int id, Event eve, EventServiceContext eventDB) =>
{
    if (id != eve.eventID)
    { 
        return Results.BadRequest();
    }

    eventDB.Entry(eve).State = EntityState.Modified;

    try
    {
        await eventDB.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!eventDB.Events.Any(e => e.eventID == id))
        {
            return Results.NotFound();
        }
        else
        {
            throw;
        }
    }

    return Results.NoContent();
});





app.Run();


