using Microsoft.AspNetCore.Components;
using Shared.Models.StakeHolders.Records;
using Shared.Models.StakeHolders.Requests;
using Shared.Models.StakeHolders.Responses;
#nullable disable
namespace FluentWeb.Pages.StakeHolders;
public partial class UpdateStakeHolder
{
    UpdateStakeHolderRequest Model = new();


    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<StakeHolderResponse, GetStakeHolderByIdRequest>(new GetStakeHolderByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,


                Email = result.Data.Email,
                PhoneNumber = result.Data.PhoneNumber,
                Area = result.Data.Area,

            };
        }
    }

}
