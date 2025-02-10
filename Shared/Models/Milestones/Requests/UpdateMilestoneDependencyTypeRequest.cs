using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Milestones.Responses;

namespace Shared.Models.Milestones.Requests
{
    public class UpdateMilestoneDependencyTypeRequest : UpdateMessageResponse, IRequest
    {

        public Guid Id { get; set; }
 

        public List<MilestoneResponse> Items { get; set; } = new();
        public Guid ProjectId { get; set; }
      

        public string EndPointName => StaticClass.Milestones.EndPoint.UpdateDependencyType;

        public string DependencyType { get; set; } = string.Empty;

       
        public override string Legend => $"Milestones";

        public override string ClassName => StaticClass.Milestones.ClassName;

    }
}
