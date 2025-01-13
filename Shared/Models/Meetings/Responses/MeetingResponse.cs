using Shared.Models.MeetingAttendants.Responses;
using Shared.Models.MeetingAgreements.Responses;
using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Meetings.Responses
{
    public class MeetingResponse : BaseResponse, IUpdateStateResponse
    {
        public Guid ProjectId { get; set; }
        public DateTime? DateofMeeting { get; set; }
        public string MeetingType { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public List<MeetingAttendantResponse> Attendants { get; set; } = new();
        public List<MeetingAgreementResponse> Agreements { get; set; } = new();
        public bool IsNodeOpen { get; set; }
        public string? Tab { get; set; } = string.Empty;
        public void Open()
        {
            IsNodeOpen = true;
        }
        public void Close()
        {
            IsNodeOpen = false;
        }

        public string EndPointName => StaticClass.Meetings.EndPoint.UpdateState;
    }
}
