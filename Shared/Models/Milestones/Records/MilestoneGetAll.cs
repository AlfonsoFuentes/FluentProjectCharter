using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Milestones.Records
{
    public class MilestoneGetAll : GetByIdMessageResponse, IGetAll
    {
        
        public string EndPointName => StaticClass.Milestones.EndPoint.GetAll;
      
        public Guid ProjectId { get; set; }

        public override string ClassName => StaticClass.Milestones.ClassName;
    }
}
