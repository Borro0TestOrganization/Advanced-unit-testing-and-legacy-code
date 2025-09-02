namespace LegacyCode._4_ClassWrap.Example.Legacy {
    public class MeetingScheduler {
        public bool Schedule(MeetingRequest meetingRequest) {
            if (meetingRequest == null || meetingRequest.Attendees.Count == 0) {
                return false;
            }

            meetingRequest.IsScheduled = true;
            meetingRequest.ScheduledAt = meetingRequest.PreferredStart;

            return true;
        }
    }

    public class MeetingRequest {
        public DateTime PreferredStart { get; set; }
        public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(30);
        public List<Attendee> Attendees { get; set; } = new List<Attendee>();
        public bool IsScheduled { get; set; }
        public DateTime? ScheduledAt { get; set; }
    }

    public class Attendee {
        public string Email { get; set; } = "";
    }

}
