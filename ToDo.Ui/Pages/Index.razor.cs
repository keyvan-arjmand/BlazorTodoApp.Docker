using MediatR;
using Microsoft.AspNetCore.Components;
using ToDo.Application.Dtos;

namespace ToDo.Ui.Pages;

public partial class Index
{
    [Parameter] public List<ToDoDto> Todos { get; set; } = default!;
    [Inject] private IMediator _mediator { set; get; }
    [CascadingParameter] public CurrentUser userInfo { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && userInfo != null)
        {
            // userInfo مقداردهی شده است و می‌توان از آن استفاده کرد
            Console.WriteLine($"User: {userInfo.UserName}, Roles: {string.Join(", ", userInfo.Roles)}");
        }
    }
}