using System;
using GameStore.API.Dtos;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpoint = "GetGame";

    private static readonly List<GameDto> games = [
        new(1,"Dofus", "MMO", 14M, new DateOnly(2000,12,12)),
        new(2,"League of Legends", "MOBA", 16M, new DateOnly(2009,10,24)),
        new(3,"FIFA 2023", "Sport", 20M, new DateOnly(2023,11,30)),
    ];

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();
        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(x => x.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpoint);

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpoint, new { game.Id }, game);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(x => x.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();

        });

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return app;
    }
}
