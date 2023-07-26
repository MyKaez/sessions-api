using System.Text.Json;
using Application.Handlers;
using Application.Models;
using Application.Services;
using Domain.Models;

namespace Application.Commands;

public static class CreateItem
{
    public record Command(Guid SessionId, JsonElement Configuration) : Request<Item>;

    public class Handler : RequestHandler<Command, Item>
    {
        private readonly ISessionService _sessionService;
        private readonly IItemService _itemService;

        public Handler(ISessionService sessionService, IItemService itemService)
        {
            _sessionService = sessionService;
            _itemService = itemService;
        }

        public override async Task<Result<Item>> Handle(
            Command request, CancellationToken cancellationToken)
        {
            var session = await _sessionService.GetById(request.SessionId, cancellationToken);

            if (session is null)
                return NotFound();

            var user = await _itemService.Create(session, request.Configuration, cancellationToken);
            
            return user;
        }
    }
}