using MediatR;
using Randim.Common.Shared.Responses;

namespace Randim.Common.Shared.Mediator;

public interface ICommand : IRequest<BaseResponse> { }

public interface ICommand<TResponse> : IRequest<BaseResponse<TResponse>> { }
