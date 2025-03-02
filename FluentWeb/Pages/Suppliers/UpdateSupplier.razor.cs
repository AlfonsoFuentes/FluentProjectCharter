using Microsoft.AspNetCore.Components;
using Shared.Models.Suppliers.Mappers;
using Shared.Models.Suppliers.Records;
using Shared.Models.Suppliers.Requests;
using Shared.Models.Suppliers.Responses;
#nullable disable
namespace FluentWeb.Pages.Suppliers;
public partial class UpdateSupplier
{
    UpdateSupplierRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<SupplierResponse, GetSupplierByIdRequest>(
            new GetSupplierByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = result.Data.ToUpdate();
        }
    }

}
