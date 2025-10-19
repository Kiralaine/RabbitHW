using RabbitMQMovieBuilder.Application.DTO_s;
using RabbitMQMovieBuilder.Application.Interfaces;
using RabbitMQMovieBuilder.Domain.Entities;

namespace RabbitMQMovieBuilder.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Movie> AddAsync(MovieDto dto)
    {
        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            FileName = dto.FileName,
            FilePath = dto.FilePath,
            FileSize = dto.FileSize,
            ContentType = dto.ContentType,
            UploadedBy = dto.UploadedBy,
            Description = dto.Description,
            IsPublic = dto.IsPublic,
            Hash = dto.Hash,
            UploadedAt = DateTime.UtcNow
        };

        return await _movieRepository.AddAsync(movie);
    }
}