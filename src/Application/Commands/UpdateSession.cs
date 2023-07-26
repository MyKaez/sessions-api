using System.Text.Json;
using Application.Handlers;
using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;

namespace Application.Commands;

public static class UpdateSession
{
    public record Command(Guid SessionId, Guid ControlId, JsonElement Configuration) : IRequest<Result<Session>>;
    
    public class Handler : RequestHandler<Command, Session>
    {
        private readonly ISessionService _sessionService;

        public Handler(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public override async Task<Result<Session>> Handle(
            Command request, CancellationToken cancellationToken)
        {
            var session = await _sessionService.GetById(request.SessionId, cancellationToken);

            if (session is null)
                return NotFound();

            if (request.ControlId != session.ControlId)
                return NotAuthorized();

            if (await _sessionService.Update(session, s =>
                {
                    //s with {Configuration = request.Configuration}
                }, cancellationToken))
                return session;

            return BadRequest("Failed to update session");
        }
    }
}