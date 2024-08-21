using GameStore.API.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpoint = "GetGame";

List<GameDto> games = [
    new(1,"Dofus", "MMO", 14M, new DateOnly(2000,12,12)),
    new(2,"League of Legends", "MOBA", 16M, new DateOnly(2009,10,24)),
    new(3,"FIFA 2023", "Sport", 20M, new DateOnly(2023,11,30)),
];

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) => games.Find(x => x.Id == id))
    .WithName(GetGameEndpoint);

app.MapPost("/game", (CreateGameDto newGame) => {
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

app.Run();
