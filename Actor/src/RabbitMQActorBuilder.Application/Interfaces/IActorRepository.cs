using RabbitMQActorBuilder.Application.DTO_s;
using RabbitMQActorBuilder.Domain.Entities;

namespace RabbitMQActorBuilder.Application.Interfaces;

public interface IActorRepository
{
    Task<Actor> AddAsync(Actor file);
}
