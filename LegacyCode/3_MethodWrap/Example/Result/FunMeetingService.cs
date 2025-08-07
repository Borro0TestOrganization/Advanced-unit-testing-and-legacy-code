namespace LegacyCode._3_MethodWrap.Example.Result {
    public class FunMeetingService {
        private readonly List<FunMeeting> _scheduledMeetings;

        public FunMeetingService() {
            _scheduledMeetings = new List<FunMeeting>();
        }

        public void ScheduleFunMeeting(FunEvent funEvent, string location) {
            if (IsTimeSpotAvailable(funEvent)) {
                AddScheduleFunMeeting(funEvent, location);
            }
        }

        private bool IsTimeSpotAvailable(FunEvent funEvent) {
            bool timeSpotAvailable = true;

            foreach (FunMeeting scheduledMeeting in _scheduledMeetings) {
                if (scheduledMeeting.Time == funEvent.Time) {
                    timeSpotAvailable = false;
                    break;
                }
            }

            return timeSpotAvailable;
        }

        private void AddScheduleFunMeeting(FunEvent funEvent, string location) {
            string formattedTitle = funEvent.Title.Trim().ToUpper();
            string formattedLocation = location.Trim();

            FunMeeting meeting = new FunMeeting(
                funEvent.Id,
                formattedTitle,
                funEvent.Description,
                funEvent.Time,
                formattedLocation
            );

            _scheduledMeetings.Add(meeting);
        }

        public List<FunMeeting> GetScheduledMeetings() {
            return _scheduledMeetings;
        }
    }

    public class FunEvent {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }

        public FunEvent(int id, string title, string description, string time) {
            Id = id;
            Title = title;
            Description = description;
            Time = time;
        }
    }

    public class FunMeeting {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }

        public FunMeeting(int id, string title, string description, string time, string location) {
            Id = id;
            Title = title;
            Description = description;
            Time = time;
            Location = location;
        }
    }
}
