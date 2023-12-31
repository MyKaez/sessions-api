﻿using System.Runtime.InteropServices.JavaScript;
using Application.Handlers;
using Application.Models;
using Application.Services;
using Domain.Models;

namespace Application.Queries;

public static class GetSessionUsers
{
    public record Query(Guid SessionId) : Request<Item[]>;

    public class Handler : RequestHandler<Query, Item[]>
    {
        private readonly ISessionService _sessionService;
        private readonly IItemService _itemService;

        public Handler(ISessionService sessionService, IItemService itemService)
        {
            _sessionService = sessionService;
            _itemService = itemService;
        }

        public override async Task<Result<Item[]>> Handle(
            Query request, CancellationToken cancellationToken)
        {
            var session = await _sessionService.GetById(request.SessionId, cancellationToken);

            if (session is null)
                return NotFound();

            var users = await _itemService.GetBySessionId(session.Id, cancellationToken);

            return users;
        }
    }
}