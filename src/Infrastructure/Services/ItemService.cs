using System.Text.Json;
using Application.Services;
using AutoMapper;
using Domain.Models;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly IUpdateService _updateService;
    private readonly IMemoryCache _memoryCache;

    public ItemService(IItemRepository itemRepository, IMapper mapper, IUpdateService updateService,
        IMemoryCache memoryCache)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
        _updateService = updateService;
        _memoryCache = memoryCache;
    }

    public async Task<Item?> GetById(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _itemRepository.GetById(userId, cancellationToken);
        var res = _mapper.Map<Item>(user);

        return res;
    }

    public async Task<Item> Create(Session session, JsonElement configuration, CancellationToken cancellationToken)
    {
        var user = new Database.SessionItem
        {
            Id = Guid.NewGuid(), 
            SessionId = session.Id,
            ControlId = Guid.NewGuid().ToString(), 
            Configuration = configuration.ToString(),
            ConnectionId = "", 
            Created = DateTime.Now, 
            Updated = DateTime.Now, 
            IsDeleted = false
        };
        var res = _mapper.Map<Item>(user);

        await _itemRepository.Create(session.Id, user, cancellationToken);

        _updateService.AddUpdate(session.Id);

        return res;
    }

    public async Task<Item[]> GetBySessionId(Guid sessionId, CancellationToken cancellationToken)
    {
        var key = sessionId.UserCacheKey();

        if (_memoryCache.TryGetValue<Item[]>(key, out var cached))
            return cached ?? Array.Empty<Item>();

        var users = await _itemRepository.GetBySessionId(sessionId, cancellationToken);
        var res = _mapper.Map<Item[]>(users);

        _memoryCache.Set(key, res, TimeSpan.FromMinutes(1));

        return res;
    }
}