using Application.Commands;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Models.Requests;
using Service.Models.Responses;

namespace Service.Controllers;

[Route("v1/sessions")]
public class SessionController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SessionController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetSessions.Query();
        var res = await _mediator.Send(query);

        return Result(res,
            session => _mapper.Map<SessionDto[]>(session).ToListDto()
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, [FromQuery] string? controlId)
    {
        var query = new GetSession.Query(id, controlId);
        var res = await _mediator.Send(query);

        return Result(res,
            session => controlId is null
                ? _mapper.Map<SessionDto>(session)
                : _mapper.Map<SessionControlDto>(session)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSessionRequest request)
    {
        var cmd = new CreateSession.Command(request.Configuration);
        var res = await _mediator.Send(cmd);

        return Result(res,
            session => _mapper.Map<SessionControlDto>(session)
        );
    }

    [HttpPost("{sessionId:guid}")]
    public async Task<IActionResult> Post(Guid sessionId, [FromBody] UpdateSessionRequest request)
    {
        var cmd = new UpdateSession.Command(sessionId, request.ControlId, request.Configuration);
        var res = await _mediator.Send(cmd);

        return Result(res,
            session => _mapper.Map<SessionDto>(session)
        );
    }

    [HttpGet("{sessionId:guid}/items")]
    public async Task<IActionResult> GetItems(Guid sessionId)
    {
        var query = new GetSessionUsers.Query(sessionId);
        var res = await _mediator.Send(query);

        return Result(res, items =>
            _mapper.Map<ItemDto[]>(items).ToListDto()
        );
    }

    [HttpPost("{sessionId:guid}/items")]
    public async Task<IActionResult> Post(Guid sessionId, [FromBody] CreateItemRequest request)
    {
        var cmd = new CreateItem.Command(sessionId, request.Configuration);
        var res = await _mediator.Send(cmd);

        return Result(res,
            item => _mapper.Map<ItemControlDto>(item)
        );
    }
}