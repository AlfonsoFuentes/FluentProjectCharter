using Microsoft.AspNetCore.Components;
#nullable disable
namespace FluentWeb.Layout;
public partial class MainLayout
{
    [CascadingParameter]
    public App App { get; set; }
 
}
