using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.Meetings.Requests;
using Shared.Models.Meetings.Responses;
using Shared.Models.Projects.Reponses;

namespace FluentWeb.Pages.Meetings;
#nullable disable
public partial class MeetingList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public ProjectResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }



    public List<MeetingResponse> Items => Parent == null ? new() : Parent.Meetings;
    string nameFilter;
    public List<MeetingResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateMeeting/{Parent.Id}");

    }

    void Edit(MeetingResponse response)
    {
        Navigation.NavigateTo($"/UpdateMeeting/{response.Id}/{Parent.Id}");
    }


   
    public async Task Delete(MeetingResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteMeetingRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.Id,
            };
            var resultDelete = await GenericService.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll.Invoke();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }

}
