using Goldix.Application.Extensions;
using Goldix.Application.Models.UserRequest;
using Goldix.Application.Queries.UserRequest;
using Goldix.Application.Wrappers;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.UserRequest;

public class GetUserRequestsBySearchQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetUserRequestsBySearchQuery, PagedResult<UserRequestDto>>
{
    public async Task<PagedResult<UserRequestDto>> Handle(GetUserRequestsBySearchQuery request, CancellationToken cancellationToken)
    {
        var status = UserRequestStatus.confirmed.ToDisplay();
        var startDate = request.dto.StartDate.ToMiladi();
        var endDate = request.dto.EndDate.ToMiladi();

        var baseQuery = db.UserRequests
            .AsNoTracking()
            .Where(x =>
                    x.Status == status &&
                    (!startDate.HasValue || x.CreatedAt.Date >= startDate.Value.Date) &&
                    (!endDate.HasValue || x.CreatedAt.Date <= endDate.Value.Date));

        var count = await baseQuery.CountAsync(cancellationToken);

        var result = await baseQuery
            .Include(x => x.Product)
            .ProjectTo<UserRequestDto>(mapper.ConfigurationProvider)
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<UserRequestDto>
        {
            Items = result,
            TotalCount = count,
            Page = request.page,
            PageSize = request.pageSize,
        };
    }
}
