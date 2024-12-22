using Microsoft.AspNetCore.Components;

namespace FluentWeb.Layout;
#nullable disable
public partial class LoginDisplay
{
    [CascadingParameter]
    public App MainApp { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (MainApp.CurrentUser == null)
        {
            await MainApp.GetCurrentUser();

            Navigation.NavigateTo("/");

            StateHasChanged();
        }


    }

   
}
