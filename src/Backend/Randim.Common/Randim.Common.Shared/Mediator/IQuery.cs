using MediatR;
using Randim.Common.Shared.Responses;

namespace Randim.Common.Shared.Mediator;

public interface IQuery<TResponse> : IRequest<BaseResponse<TResponse>> { }
