using RabbitMQMovieListener.Application.Interfaces;
using RabbitMQMovieListener.Domain.Entities;

namespace RabbitMQMovieListener.Application.Sevices;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repository;

    public MovieService(IMovieRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Movie movie)
    {
        await _repository.AddAsync(movie);
    }
}
