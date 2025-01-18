using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.OrganizationStrategies.Responses
{
    public class EngineeringFluidCodeResponseList : IResponseAll
    {
        public List<EngineeringFluidCodeResponse> Items { get; set; } = new();
    }
}
