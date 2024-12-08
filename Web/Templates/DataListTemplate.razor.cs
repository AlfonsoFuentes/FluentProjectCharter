using Microsoft.AspNetCore.Components;
using Shared.Models.BaseResponses;
using Shared.Models.Cases.Requests;
using Shared.Models.Cases.Responses;
using Web.Infrastructure.Managers.Generic;

#nullable disable
namespace Web.Templates;
public partial class DataListTemplate<T> where T : class
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    public EventCallback<T> OnDelete { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    bool AddItem = false;


    public void CancelAsync()
    {
        AddItem = false;
        Selected = null!;
        StateHasChanged();
    }
    void Add()
    {
        AddItem = true;
    }
    [Parameter]
    public T Selected { get; set; } = null!;
    [Parameter]
    public EventCallback<T> SelectedChanged { get; set; }

    [Parameter]
    public string NameFilter {  get; set; }
    [Parameter]
    public EventCallback<string> NameFilterChanged {  get; set; }
    async Task Edit(T response)
    {
        Selected = response;
        await SelectedChanged.InvokeAsync(Selected);    
    }

    async Task Delete(T caseResponse)
    {
        await OnDelete.InvokeAsync(caseResponse);
    }
    [Parameter]
    public RenderFragment Create {  get; set; }
    [Parameter]
    public RenderFragment<T> Update { get; set; }
    [Parameter]
    public RenderFragment DataList { get; set; }
}
