using RabbitMQActorBuilder.Application.DTO_s;
using RabbitMQActorBuilder.Domain.Entities;

namespace RabbitMQActorBuilder.Application.Services;

public interface IActorService
{
    Task<Actor> AddAsync(ActorDto dto);
}