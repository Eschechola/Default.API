using System.Threading.Tasks;
using Default.API.Authorization.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Default.API.Authorization.Extensions;
using Default.ApplicationServices.VIewModels.Generic;
using Default.ApplicationServices.VIewModels.User;
using Default.Domain.DTOs.Authorization;
using Default.Domain.DTOs.Core.Events;
using Default.Domain.Interfaces.ApplicationServices;

namespace Default.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(
    ITokenService tokenService,
    IUserApplicationService userApplicationService,
    INotificationHandler<DomainNotification> handler) : BaseController(handler)
{
    /// <summary>
    /// Retrieves logged user data
    /// </summary>
    /// <response code="200">User Authorized</response>
    /// <response code="401">User Unauthorized</response>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(ResponseViewModel<object>), 401)]
    [ProducesResponseType(typeof(ResponseViewModel<object>), 200)]
    public IActionResult Me()
        => Ok(new
        {
            Id = User.GetId(),
            Role = User.GetRole(),
            Jti = User.GeTokenIdentifier(),
            Iat = User.GetTokenIssuedAt()
        });
    
    /// <summary>
    /// Login User
    /// </summary>
    /// <response code="200">User Authorized</response>
    /// <response code="401">User Unauthorized</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseViewModel<LoginDto>), 200)]
    [ProducesResponseType(typeof(ResponseViewModel<object>), 401)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(model);

        var user = await userApplicationService.LoginAsync(model.Username, model.Password);

        return user.IsEmpty 
            ? Response()
            : Response(new
            {
                User = user.Value,
                Token = tokenService.CreateToken(user.Value.Id, user.Value.Username)
            }, "User logged with success");
    }
}