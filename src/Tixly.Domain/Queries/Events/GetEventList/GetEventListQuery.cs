using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Tixly.Domain.Dtos;

namespace Tixly.Domain.Queries.Events.GetEventList
{
    public record GetEventListQuery(PaginationRequest PaginationRequest) : IQuery<GetEventListResult>;
    public record GetEventListResult(Result<PaginatedResult<EventDto>> CommandResult);
}
