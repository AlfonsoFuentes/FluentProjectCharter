namespace Shared.Models.Projects.Reponses
{
    public class ProjectResponseList
    {
        public ProjectResponse? CurrentProject { get; set; } = null!;
        public List<ProjectResponse> Items { get; set; } = new();
    }
}
