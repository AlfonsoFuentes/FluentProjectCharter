using Microsoft.AspNetCore.Components;
using Shared.Models.BaseResponses;
using Shared.Models.FileResults.Generics.Reponses;
#nullable disable
namespace FluentWeb.SharedComponents;
public partial class TemplatedList<T> where T : IResponse
{
    [Parameter]
    public string Title { get; set; } = string.Empty;
    [Parameter, EditorRequired]
    public RenderFragment<T> Content { get; set; } = null!;
    [Parameter, EditorRequired]
    public RenderFragment<T> Buttons { get; set; } = null!;
    [Parameter, EditorRequired]
    public RenderFragment MenuButtons { get; set; } = null!;
    [Parameter, EditorRequired]
    public List<T> FilteredItems { get; set; } = new(); 
    [Parameter]
    public RenderFragment<T> ContentNode { get; set; } = null!;

}
