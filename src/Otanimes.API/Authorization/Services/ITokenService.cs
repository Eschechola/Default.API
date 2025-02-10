using System;
using Otanimes.Domain.DTOs.Authorization;

namespace Otanimes.API.Authorization.Services;

public interface ITokenService
{
    TokenDto CreateToken(Guid id, string username);
}