namespace Shared.Models.Backgrounds.Responses
{
    public class BackGroundResponse : BaseResponse
    {
        public Guid CaseId { get; set; }
   
        public Guid ProjectId { get; set; }
    }
}
