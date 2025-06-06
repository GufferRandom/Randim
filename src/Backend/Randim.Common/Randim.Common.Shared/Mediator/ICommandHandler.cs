using MediatR;
using Randim.Common.Shared.Responses;

namespace Randim.Common.Shared.Mediator;

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, BaseResponse<TResponse>>
    where TCommand : ICommand<TResponse> { }
