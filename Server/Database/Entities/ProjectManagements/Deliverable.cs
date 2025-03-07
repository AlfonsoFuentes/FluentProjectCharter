namespace Server.Database.Entities.ProjectManagements
{
    public class Deliverable : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GanttTask> GanttTasks { get; set; } = new();
        public static Deliverable Create(Guid projectId, int order)
        {
            return new()
            {
                ProjectId = projectId,
                Order = order,
            };
        }
        public bool IsExpanded {  get; set; }
    }

}
