using RabbitMQMovieBuilder.Application.DTO_s;
using RabbitMQMovieBuilder.Domain.Entities;

namespace RabbitMQMovieBuilder.Application.Interfaces;

public interface IMovieRepository
{
    Task<Movie> AddAsync(Movie file);
}
