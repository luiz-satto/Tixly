using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Tixly.Domain.Dtos;
using Tixly.Domain.Extensions;
using Tixly.Infrastructure.Repositories.Event;

namespace Tixly.Domain.Queries.Events.GetEventList
{
    public class GetEventListHandler(IEventRepository eventRepository)
        : IQueryHandler<GetEventListQuery, GetEventListResult>
    {
        public async Task<GetEventListResult> Handle(GetEventListQuery query, CancellationToken cancellationToken)
        {
            int pageIndex = query.PaginationRequest.PageIndex;
            int pageSize = query.PaginationRequest.PageSize;
            var events = await eventRepository.GetAsync(pageIndex, pageSize, cancellationToken);
            IEnumerable<EventDto> eventsDto = events != null ? events.ToEventDtoList() : default!;
            PaginatedResult<EventDto> result = new(pageIndex, pageSize, eventsDto.Count(), eventsDto);
            return new GetEventListResult(Result.Success(result));
        }
    }
}
