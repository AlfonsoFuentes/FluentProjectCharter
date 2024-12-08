using Shared.Models.Cases.Responses;

namespace Shared.Models.Projects.Reponses
{
    public class ProjectResponse : BaseResponse
    {
    
        public List<CaseResponse> Cases { get; set; } = new();
    }
}
