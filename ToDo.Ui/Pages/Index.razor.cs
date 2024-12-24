using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using ToDo.Application.Dtos;
using ToDo.Application.ToDoList.Queries.GetAllToDoList;
using ToDo.Application.Users.Queries.GetAllUsers;
using ToDo.Domain.Entity;
using ToDo.Ui.Services;

namespace ToDo.Ui.Pages;

public partial class Index
{
    [Parameter] public List<ToDoDto> Todos { get; set; } = default!;
    [Parameter] public List<UserDto> Users { get; set; } = default!;
    [Parameter] public int NewTodo { get; set; } = 0;
    [Parameter] public int CurrentTodo { get; set; }  = 0;
    [Parameter] public int DeadLineToDo { get; set; } = 0;
    private bool isAuth;
    private bool isAdmin;
    [Inject] private IMediator _mediator { set; get; }
    [CascadingParameter] public CurrentUser userInfo { get; set; } = new();
    [Inject] IJSRuntime JSRuntime { get; set; }
    [Inject] private JwtAuthenticationStateProvider AuthStateProvider { set; get; }
    [Inject] private NavigationManager Navigation { set; get; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { set; get; }


    protected override async Task OnInitializedAsync()
    {
        var token = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "token");
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            userInfo.UserName = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)
                ?.Value;
            userInfo.UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)
                ?.Value;
            userInfo.Roles = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
                .ToList();
        }
        else
        {
            Navigation.NavigateTo("/login",true);
        }
        
        isAdmin =  userInfo.Roles.Contains("Admin");
        if (isAdmin)
        {
            Users = await _mediator.Send(new GetAllUsersQuery(), CancellationToken.None);
        }
    }
}