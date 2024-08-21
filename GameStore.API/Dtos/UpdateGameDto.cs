using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos;

public record class UpdateGameDto(
    [Required][MaxLength(50)] string Name, 
    [Required][MaxLength(20)] string Genre,
    [Required][Range(1,100)] decimal Price,
    DateOnly ReleaseDate
);