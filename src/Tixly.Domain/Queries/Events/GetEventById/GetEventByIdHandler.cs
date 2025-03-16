using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Dtos;
using Tixly.Domain.Extensions;
using Tixly.Infrastructure.Models;
using Tixly.Infrastructure.Repositories.Event;

namespace Tixly.Domain.Queries.Events.GetEventById
{
    public class GetEventByIdHandler(IEventRepository eventRepository)
        : IQueryHandler<GetEventByIdQuery, GetEventByIdResult>
    {
        public async Task<GetEventByIdResult> Handle(GetEventByIdQuery query, CancellationToken cancellationToken)
        {
            Event @event = await eventRepository.GetAsync(query.Id, cancellationToken);
            if (@event == null)
            {
                return new GetEventByIdResult(Result.Failure<EventDto>(Error.NotFound));
            }

            return new GetEventByIdResult(Result.Success(@event.ToEventDto()));
        }
    }
}
