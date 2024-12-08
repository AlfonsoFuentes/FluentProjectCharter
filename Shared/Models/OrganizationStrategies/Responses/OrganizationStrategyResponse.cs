using Shared.Models.Cases.Responses;

namespace Shared.Models.OrganizationStrategies.Responses
{
    public class OrganizationStrategyResponse : BaseResponse
    {

        public List<CaseResponse> Cases { get; set; } = new();
    }
}
