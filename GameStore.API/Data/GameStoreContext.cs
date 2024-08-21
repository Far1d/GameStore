using System;
using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    DbSet<Game> Games => Set<Game>();
    DbSet<Genre> Genres => Set<Genre>();
}
