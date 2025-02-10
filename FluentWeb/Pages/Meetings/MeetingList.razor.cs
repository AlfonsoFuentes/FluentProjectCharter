namespace FluentWeb.Pages.Meetings;
#nullable disable
public partial class MeetingList
{
    //[CascadingParameter]
    //public App App { get; set; }
    //[Parameter]
    //[EditorRequired]
    //public ProjectResponse Parent { get; set; } = new();
    //[Parameter]
    //[EditorRequired]
    //public Func<Task> GetAll { get; set; }



    //public List<MeetingResponse> Items => Parent == null ? new() : Parent.Meetings;
    //string nameFilter;
    //public List<MeetingResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    //public void AddNew()
    //{
    //    Navigation.NavigateTo($"/CreateMeeting/{Parent.Id}");

    //}

    //void Edit(MeetingResponse response)
    //{
    //    Navigation.NavigateTo($"/UpdateMeeting/{response.Id}/{Parent.Id}");
    //}
    //async Task GetCurrentMeeting()
    //{
    //    try
    //    {
    //        var request = new GetMeetingByIdRequest
    //        {
    //            Id = Parent.CurrentMeeting.Id,
    //        };
    //        var result = await GenericService.GetById<MeetingResponse, GetMeetingByIdRequest>(request).ConfigureAwait(false);
    //        if (result.Succeeded)
    //        {
    //            App.ProjectList.CurrentProject.CurrentMeeting = result.Data;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Manejo de errores
    //        Console.Error.WriteLine($"Error al obtener el proyecto actual: {ex.Message}");
    //    }
    //}


    //public async Task Delete(MeetingResponse response)
    //{
    //    var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
    //    var result = await dialog.Result;
    //    var canceled = result.Cancelled;



    //    if (!canceled)
    //    {
    //        DeleteMeetingRequest request = new()
    //        {
    //            Id = response.Id,
    //            Name = response.Name,
    //            ProjectId = Parent.Id,
    //        };
    //        var resultDelete = await GenericService.Delete(request);
    //        if (resultDelete.Succeeded)
    //        {
    //            await GetAll.Invoke();
    //            _snackBar.ShowSuccess(resultDelete.Messages);


    //        }
    //        else
    //        {
    //            _snackBar.ShowError(resultDelete.Messages);
    //        }
    //    }

    //}
    //public async Task Show(MeetingResponse response)
    //{
    //    if (App.CurrentProject == null) return;
    //    if (App.CurrentMeeting == null)
    //    {
    //        App.CurrentProject.CurrentMeeting = response;
    //        response.Open();
    //        await GenericService.UpdateState(response);
    //        await GetCurrentMeeting();

    //    }
    //    else if (App.CurrentMeeting.Id == response.Id)
    //    {
    //        response.Close();
    //        App.ProjectList.CurrentProject.CurrentMeeting = null!;
    //        await GenericService.UpdateState(response);
    //    }
    //    else
    //    {
    //        App.CurrentMeeting.Close();
    //        await GenericService.UpdateState(App.CurrentMeeting);
    //        App.ProjectList.CurrentProject.CurrentMeeting = response;

    //        response.Open();
    //        await GenericService.UpdateState(response);
    //        await GetCurrentMeeting();
    //    }
    //    StateHasChanged();

    //}
}
