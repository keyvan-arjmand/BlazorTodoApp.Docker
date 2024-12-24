using ToDo.Application.Dtos;
using ToDo.Domain.Entity;

namespace ToDo.Application.Users.Queries.GetAllRoles;

public record GetAllRolesQuery():IRequest<List<RoleDto>>;