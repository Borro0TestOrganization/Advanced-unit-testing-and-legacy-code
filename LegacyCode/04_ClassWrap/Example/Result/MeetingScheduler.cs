namespace LegacyCode._4_ClassWrap.Example.Result {
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

    public class MeetingSchedulerBusinessHours {
        private MeetingScheduler _meetingScheduler;

        public MeetingSchedulerBusinessHours(MeetingScheduler meetingScheduler) { 
            _meetingScheduler = meetingScheduler;
        }

        public bool Schedule(MeetingRequest meetingRequest) {
            if (meetingRequest == null) {
                return false;
            }

            DateTime preferredStart = meetingRequest.PreferredStart;

            bool workDay = preferredStart.DayOfWeek >= DayOfWeek.Monday 
                && preferredStart.DayOfWeek <= DayOfWeek.Friday;

            bool withinWorkHours = preferredStart.Hour >= 9 
                && preferredStart.Hour < 17 
                || (preferredStart.Hour == 16 && preferredStart.Minute <= 59);

            if (workDay && withinWorkHours) {
                return _meetingScheduler.Schedule(meetingRequest);
            } else {
                return false;
            }
        }
    }

    public class MeetingRequest {
        public DateTime PreferredStart { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Attendee> Attendees { get; set; }
        public bool IsScheduled { get; set; }
        public DateTime? ScheduledAt { get; set; }

        public MeetingRequest() {
            Duration = TimeSpan.FromMinutes(30);
            Attendees = new List<Attendee>();
        }
    }

    public class Attendee {
        public string Email { get; set; }

        public Attendee() {
            Email = "";
        }
    }

}
