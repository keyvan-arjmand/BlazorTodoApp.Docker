using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Dtos;
using ToDo.Application.Users.Queries.GetAllRoles;
using ToDo.Application.Users.Queries.GetAllUsers;
using ToDo.Domain.Entity;

namespace ToDo.Ui.Pages;

public partial class ManageUsers : ComponentBase
{
    [Parameter] public List<UserDto> Users { get; set; } = new();
    [Parameter] public List<RoleDto> Roles { get; set; } = new();
    [Inject] private IMediator _mediator { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Roles.AddRange(new[]
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
        Users = await _mediator.Send(new GetAllUsersQuery(), CancellationToken.None);
    }
}