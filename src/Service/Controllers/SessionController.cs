using Application.Commands;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using Service.Models.Requests;

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
    public async Task<IActionResult> Get()
    {
        var query = new GetSessions.Query();
        var res = await _mediator.Send(query);

        return Result(res, session => _mapper.Map<SessionDto[]>(session));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, [FromQuery] string? controlId)
    {
        var query = new GetSession.Query(id, controlId);
        var res = await _mediator.Send(query);

        return Result(res, session => _mapper.Map<SessionDto>(session));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SessionRequest request)
    {
        var cmd = new RegisterSession.Command(request.Name, request.Configuration);
        var res = await _mediator.Send(cmd);

        return Result(res, session => _mapper.Map<SessionControlDto>(session));
    }

    [HttpPost("{sessionId:guid}")]
    public async Task<IActionResult> Post(Guid sessionId, [FromBody] SessionUpdateRequest request)
    {
        var cmd = new UpdateSession.Command(sessionId, request.ControlId, request.Configuration);
        var res = await _mediator.Send(cmd);

        return Result(res, session => _mapper.Map<SessionDto>(session));
    }

    [HttpGet("{sessionId:guid}/users")]
    public async Task<IActionResult> GetUsers(Guid sessionId)
    {
        var query = new GetSessionUsers.Query(sessionId);
        var res = await _mediator.Send(query);

        return Result(res, user => _mapper.Map<UserDto[]>(user));
    }

    [HttpPost("{sessionId:guid}/users")]
    public async Task<IActionResult> Post(Guid sessionId, [FromBody] UserRequest request)
    {
        var cmd = new RegisterUser.Command(sessionId, request.Name);
        var res = await _mediator.Send(cmd);

        return Result(res, user => _mapper.Map<UserControlDto>(user));
    }
}