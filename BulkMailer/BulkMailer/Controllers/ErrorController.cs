﻿using Microsoft.AspNetCore.Mvc;

namespace BulkMailer.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("error")]
    public IActionResult Error()
    {
        return Problem();
    }
}