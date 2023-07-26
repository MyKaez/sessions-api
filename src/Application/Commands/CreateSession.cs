using System.Text.Json;
using Application.Handlers;
using Application.Models;
using Application.Services;
using Domain.Models;

namespace Application.Commands;

public static class CreateSession
{
    public record Command(JsonElement? Configuration) : Request<Session>;

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
            var session = await _sessionService.CreateSession(request.Configuration, cancellationToken);

            return session ?? NotFound();
        }
    }
}