using Microsoft.AspNetCore.Components;
using Shared.Models.MeetingAttendants.Requests;
using Shared.Models.MeetingAttendants.Responses;
using Shared.Models.Meetings.Responses;

namespace FluentWeb.Pages.MeetingAttendants;
#nullable disable
public partial class MeetingAttendantList
{
    //[CascadingParameter]
    //public App App { get; set; }
    //[Parameter]
    //[EditorRequired]
    //public MeetingResponse Parent { get; set; } = new();
    //[Parameter]
    //[EditorRequired]
    //public Func<Task> GetAll { get; set; }



    //public List<MeetingAttendantResponse> Items => Parent == null ? new() : Parent.Attendants;
    //string nameFilter;
    //public List<MeetingAttendantResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    //public void AddNew()
    //{
    //    Navigation.NavigateTo($"/CreateMeetingAttendant/{Parent.Id}/{Parent.ProjectId}");

    //}

    //void Edit(MeetingAttendantResponse response)
    //{
    //    Navigation.NavigateTo($"/UpdateMeetingAttendant/{response.Id}");
    //}


   
    //public async Task Delete(MeetingAttendantResponse response)
    //{
    //    //var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
    //    //var result = await dialog.Result;
    //    //var canceled = result.Cancelled;



    //    //if (!canceled)
    //    //{
    //    //    DeleteMeetingAttendantRequest request = new()
    //    //    {
    //    //        Id = response.Id,
    //    //        Name = response.Name,
    //    //        ProjectId = Parent.Id,
    //    //    };
    //    //    var resultDelete = await GenericService.Delete(request);
    //    //    if (resultDelete.Succeeded)
    //    //    {
    //    //        await GetAll.Invoke();
    //    //        _snackBar.ShowSuccess(resultDelete.Messages);


    //    //    }
    //    //    else
    //    //    {
    //    //        _snackBar.ShowError(resultDelete.Messages);
    //    //    }
    //    //}

    //}

}
