using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Milestones.Records
{
   public class GetMilestoneByIdRequest : GetByIdMessageResponse, IGetById
    {
   
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Milestones.EndPoint.GetById;
        public override string ClassName => StaticClass.Milestones.ClassName;
    }

}
