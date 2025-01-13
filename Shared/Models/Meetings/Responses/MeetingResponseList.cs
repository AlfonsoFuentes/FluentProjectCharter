namespace Shared.Models.Meetings.Responses
{
    public class MeetingResponseList
    {
        public MeetingResponse CurrentMeeting { get; set; } = null!;
        public List<MeetingResponse> Items { get; set; } = new();
    }
}
