using System;
using Default.Domain.DTOs.Authorization;

namespace Default.API.Authorization.Services;

public interface ITokenService
{
    TokenDto CreateToken(Guid id, string username);
}