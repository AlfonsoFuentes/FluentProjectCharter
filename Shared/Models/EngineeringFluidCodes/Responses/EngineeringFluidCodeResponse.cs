using Shared.Models.Cases.Responses;

namespace Shared.Models.OrganizationStrategies.Responses
{
    public class EngineeringFluidCodeResponse : BaseResponse
    {
        public string Code { get; set; } = string.Empty;
        public List<CaseResponse> Cases { get; set; } = new();
    }
}
