using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ToDo.Application.Users.Commands.InsertUser;

namespace ToDo.Ui.Pages;

public partial class Signup
{
    [Parameter] public string PhoneNumber { get; set; } = string.Empty;
    [Parameter] public string Name { get; set; } = string.Empty;
    [Parameter] public string Family { get; set; } = string.Empty;
    [Parameter] public string Password { get; set; } = string.Empty;
    [Inject] private IJSRuntime JSRuntime { get; set; }
    [Inject] private IMediator _mediator { set; get; }
    [Inject] private NavigationManager Navigation { get; set; }


    private async Task RegisterUser()
    {
        try
        {
            await JSRuntime.InvokeVoidAsync("Notiflix.Loading.circle", "در حال پردازش ...");

            var result = await _mediator.Send(new InsertUserCommand
            {
                Family = Family,
                PhoneNumber = PhoneNumber,
                Name = Name,
                Pass = Password
            }, CancellationToken.None);
            if (result.IsSuccess)
            {
                ShowSuccess();
                await JSRuntime.InvokeVoidAsync("Notiflix.Loading.remove");
                Navigation.NavigateTo("/login",true);
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("Notiflix.Loading.remove");
                ShowError($"خطا در ثبت نام کاربر");
            }
        }
        catch (Exception e)
        {
            await JSRuntime.InvokeVoidAsync("Notiflix.Loading.remove");
            ShowError(e.Message);
        }
    }

    private async Task ShowSuccess()
    {
        await JSRuntime.InvokeVoidAsync("Swal.fire", new
        {
            title = "موفقیت!",
            text = "ثبت نام موفقیت آمیز بود.",
            icon = "success",
            confirmButtonText = "باشه"
        });
    }

    private async Task ShowError(string message)
    {
        await JSRuntime.InvokeVoidAsync("Swal.fire", new
        {
            title = "خطا!",
            text = message,
            icon = "error",
            confirmButtonText = "باشه"
        });
    }
}