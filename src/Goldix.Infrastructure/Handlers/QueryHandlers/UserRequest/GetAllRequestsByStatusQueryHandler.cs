using Goldix.Application.Models.UserRequest;
using Goldix.Application.Queries.UserRequest;
using Goldix.Application.Wrappers;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.UserRequest;

public class GetAllRequestsByStatusQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllUserRequestsByStatusQuery, PagedResult<UserRequestDto>>
{
    public async Task<PagedResult<UserRequestDto>> Handle(GetAllUserRequestsByStatusQuery request, CancellationToken cancellationToken)
    {
        var status = (request.dto.Status).ToDisplay();

        var baseQuery = db.UserRequests
            .AsNoTracking()
            .Where(x => x.Status == status && x.User.IsActive);

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
