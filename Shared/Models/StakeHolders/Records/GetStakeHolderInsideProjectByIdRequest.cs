using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.StakeHolders.Records
{
    public class GetStakeHolderInsideProjectByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.StakeHolders.EndPoint.GetByIdInsideProject;
        public override string ClassName => StaticClass.StakeHolders.ClassName;
        public Guid ProjectId {  get; set; }
    }

}
