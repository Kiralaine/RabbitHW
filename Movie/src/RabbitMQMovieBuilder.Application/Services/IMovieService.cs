using RabbitMQMovieBuilder.Application.DTO_s;
using RabbitMQMovieBuilder.Domain.Entities;

namespace RabbitMQMovieBuilder.Application.Services;

public interface IMovieService
{
    Task<Movie> AddAsync(MovieDto dto);
}