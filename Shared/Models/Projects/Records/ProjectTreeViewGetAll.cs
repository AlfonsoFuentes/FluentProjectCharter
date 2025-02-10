using Shared.Models.FileResults.Generics.Records;

namespace Shared.Models.Projects.Records
{
    public class ProjectTreeViewGetAll : IGetAll
    {
        public string EndPointName => StaticClass.Projects.EndPoint.GetAllTreeView;
    }
}
