using RabbitMQMovieListener.Domain.Entities;

namespace RabbitMQMovieListener.Application.Sevices;

public interface IMovieService
{
    Task CreateAsync(Movie movie);
}
