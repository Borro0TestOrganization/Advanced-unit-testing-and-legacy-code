namespace LegacyCode._2_ClassSprout.Example.Result {
    public class TrainingScheduler {
        private readonly TrainingLocationScheduler _locationScheduler;

        public TrainingScheduler(TrainingLocationScheduler locationScheduler) {
            _locationScheduler = locationScheduler;
        }

        public void CreateAgendaMeetings(TrainingDetail trainingDetails) {
            foreach (Training training in trainingDetails.TrainingDays) {
                AgendaMeeting agendaMeeting = new AgendaMeeting(
                    training.Day, training.StartsAt, training.EndsAt);

                training.AddMeeting(agendaMeeting);
            }

            _locationScheduler.SetMeetingLocations(trainingDetails.TrainingDays);
        }
    }

    public class TrainingLocationScheduler {
        public void SetMeetingLocations(IList<Training> trainings) {
            foreach (Training training in trainings) {
                switch (training.GetLocationType()) {
                    case LocationType.Offline:
                        training.AgendaMeeting.SetLocation(training.Location);
                        break;
                    case LocationType.Online:
                        training.AgendaMeeting.SetMeetingURL(training.Location);
                        break;
                }
            }
        }
    }

    public class TrainingDetail {
        public IList<Training> TrainingDays { get; internal set; }

        public TrainingDetail(IList<Training> trainingDays) {
            TrainingDays = trainingDays;
        }
    }

    public enum LocationType {
        Online,
        Offline
    }

    public class Training {
        public string Day { get; internal set; }
        public string StartsAt { get; internal set; }
        public string EndsAt { get; internal set; }
        public string Location { get; internal set; }
        public LocationType LocationType { get; internal set; }
        public AgendaMeeting AgendaMeeting { get; internal set; }

        public Training(string day, string startsAt, string endsAt, string location, LocationType locationType) {
            Day = day;
            StartsAt = startsAt;
            EndsAt = endsAt;
            Location = location;
            LocationType = locationType;
        }

        internal void AddMeeting(AgendaMeeting agendaMeeting) {
            AgendaMeeting = agendaMeeting;
        }

        internal LocationType GetLocationType() {
            return LocationType;
        }
    }

    public class AgendaMeeting {
        public string Day { get; internal set; }
        public string StartsAt { get; internal set; }
        public string EndsAt { get; internal set; }
        public string Location { get; internal set; }
        public string MeetingURL { get; internal set; }

        public AgendaMeeting(string day, string startsAt, string endsAt) {
            Day = day;
            StartsAt = startsAt;
            EndsAt = endsAt;
        }

        internal void SetLocation(string location) {
            Location = location;
        }

        internal void SetMeetingURL(string meetingURL) {
            MeetingURL = meetingURL;
        }
    }
}
