using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Assumptions.Responses
{
    public class AssumptionResponseList : IResponseAll
    {
        public List<AssumptionResponse> Items { get; set; } = new();
        public string ProjectName { get; set; } = string.Empty;
    }
}
