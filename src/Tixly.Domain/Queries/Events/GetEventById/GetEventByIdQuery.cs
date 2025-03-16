using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Dtos;

namespace Tixly.Domain.Queries.Events.GetEventById
{
    public record GetEventByIdQuery(Guid Id) : IQuery<GetEventByIdResult>;
    public record GetEventByIdResult(Result<EventDto> CommandResult);
}
