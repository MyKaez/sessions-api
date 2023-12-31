﻿using System.Collections;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Models.Responses;
using NotFoundResult = Application.Models.NotFoundResult;

namespace Service.Controllers;

public abstract class BaseController : Controller
{
    protected IActionResult Result<T>(Result<T> result, Func<T, object> ok)
    {
        return result.Match<IActionResult>(
            res =>
            {
                var dto = ok(res);
                return base.Ok(dto);
            },
            err =>
            {
                if (ReferenceEquals(err, NotFoundResult.Obj))
                    return base.NotFound();

                if (ReferenceEquals(err, NotAuthorizedResult.Obj))
                    return base.Unauthorized();

                if (err is BadRequest br)
                    return base.BadRequest(br);

                return base.Problem();
            });
    }
}