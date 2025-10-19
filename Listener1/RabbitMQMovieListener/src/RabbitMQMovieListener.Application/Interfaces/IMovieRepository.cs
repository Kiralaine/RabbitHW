using RabbitMQMovieListener.Domain.Entities;

namespace RabbitMQMovieListener.Application.Interfaces;

public interface IMovieRepository
{
    Task AddAsync(Movie movie);
}