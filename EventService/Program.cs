using EventService.Models;
using Microsoft.EntityFrameworkCore;

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
    await eventDB.Events.FindAsync(id) is Event eventById ? Results.Ok(eventById) : Results.NotFound()
);

// adds event by POST
events.MapPost("/", async (Event eve, EventServiceContext eventDB) =>
{
    eventDB.Events.Add(eve);
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


