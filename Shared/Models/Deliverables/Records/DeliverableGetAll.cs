using Shared.Models.FileResults.Generics.Records;

namespace Shared.Models.Deliverables.Records
{
    public class DeliverableGetAll : IGetAll
    {
      
        public string EndPointName => StaticClass.Deliverables.EndPoint.GetAll;
        public Guid ProjectId { get; set; }
    
    }
}
