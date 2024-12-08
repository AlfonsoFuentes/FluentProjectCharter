namespace Shared.Models.StakeHolders.Responses
{
    public class StakeHolderResponse : BaseResponse
    {
        public Guid CaseId { get; set; }

        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
    }
}
