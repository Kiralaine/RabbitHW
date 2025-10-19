using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQMovieListener.Application.Interfaces;
using RabbitMQMovieListener.Domain.Entities;

namespace RabbitMQMovieListener.Infrastructure.Persistence.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
    }

}
