using MediatR;
using Randim.Common.Shared.Responses;

namespace Randim.Common.Shared.Mediator;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, BaseResponse<TResponse>>
    where TQuery : IQuery<TResponse> { }
