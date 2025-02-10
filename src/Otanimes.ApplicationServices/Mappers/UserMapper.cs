using Otanimes.Domain.DTOs.Entities;
using Otanimes.Domain.Entities;

namespace Otanimes.ApplicationServices.Mappers;

public static class UserMapper
{
    public static UserDto AsDto(this User? entity)
        => entity is null
            ? null
            : new UserDto
            {
                Id = entity.Id,
                Username = entity.Username,
                LastLoginDate = entity.LastLoginDate,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
}