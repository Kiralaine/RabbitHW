using RabbitMQActorBuilder.Application.DTO_s;
using RabbitMQActorBuilder.Application.Interfaces;
using RabbitMQActorBuilder.Domain.Entities;

namespace RabbitMQActorBuilder.Application.Services;

public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;

    public ActorService(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public async Task<Actor> AddAsync(ActorDto dto)
    {
        var actor = new Actor
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

        return await _actorRepository.AddAsync(actor);
    }
}