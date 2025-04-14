using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Deliverables.Responses
{
    public class DeliverableResponseList : IResponseAll
    {
        public Guid ProjectId { get; set; }
        public List<DeliverableResponse> Items { get; set; } = new();
    
    }
}
