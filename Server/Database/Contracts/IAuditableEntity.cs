namespace Server.Database.Contracts
{
    public interface ICacheConstants
    {
        string[] CacheKeys { get; }
    }
    public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId>
    {
    }

    public interface IAuditableEntity : IEntity, ISoftDeletable
    {

        string? CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? LastModifiedOn { get; set; }
        string? LastModifiedBy { get; set; }
    }
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOnUtc { get; set; }
    }
}