using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Mapping;
using ToDo.Application.Dtos;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entity;

namespace ToDo.Application.Users.Queries.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IUnitOfWork _work;

    public GetAllRolesQueryHandler(RoleManager<Role> roleManager, IUnitOfWork work)
    {
        _roleManager = roleManager;
        _work = work;
    }

    public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        //var roles = await _work.GenericRepository<Role>().TableNoTracking.Select(x => new RoleDto
        //{
        //    Id = x.Id,
        //    Name = x.Name!
        //}).ToListAsync(cancellationToken);
        //var map= roles.ToDto<RoleDto>().ToList();
        
        var roles = new List<RoleDto>();
        roles.AddRange(new[]
        {
            new RoleDto()
            {
                Id = 1,
                Name = "Admin"
            },
            new RoleDto
            {
                Id = 2,
                Name = "User"
            }
        });
        return roles;
    }
}