using Shared.Enums.ProjectNeedTypes;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.StakeHolders.Responses;
using Shared.StaticClasses;

namespace Shared.Models.Projects.Request
{
    public class UpdateProjectRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Projects.EndPoint.Update;
        public ProjectNeedTypeEnum ProjectNeedType { get; set; } = ProjectNeedTypeEnum.None;
        public override string Legend => Name;
        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
        public override string ClassName => StaticClass.Projects.ClassName;
        public StakeHolderResponse Manager { get; set; } = null!;
        public StakeHolderResponse Sponsor { get; set; } = null!;
        public DateTime? InitialProjectDate { get; set; } = DateTime.Today;

    
        public double PercentageTaxProductive { get; set; }
        public bool IsProductiveAsset { get; set; } = true;
    }
}
