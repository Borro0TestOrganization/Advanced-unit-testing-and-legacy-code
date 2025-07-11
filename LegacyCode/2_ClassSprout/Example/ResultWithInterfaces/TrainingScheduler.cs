namespace LegacyCode._2_ClassSprout.Example.ResultWithInterfaces {
    public class TrainingScheduler {
        private readonly ITrainingScheduler _agendaMeetingScheduler;
        private readonly ITrainingScheduler _locationScheduler;

        public TrainingScheduler(ITrainingScheduler agendaMeetingScheduler, ITrainingScheduler locationScheduler) {
            _agendaMeetingScheduler = agendaMeetingScheduler;
            _locationScheduler = locationScheduler;
        }

        public void CreateAgendaMeetings(TrainingDetail trainingDetails) {
            _agendaMeetingScheduler.Schedule(trainingDetails);
            _locationScheduler.Schedule(trainingDetails);
        }
    }

    public interface ITrainingScheduler {
        void Schedule(TrainingDetail trainingDetails);
    }

    public class TrainingAgendaMeetingScheduler : ITrainingScheduler {
        public void Schedule(TrainingDetail trainingDetails) {
            foreach (Training training in trainingDetails.TrainingDays) {
                AgendaMeeting agendaMeeting = new AgendaMeeting(
                    training.Day, training.StartsAt, training.EndsAt);

                training.AddMeeting(agendaMeeting);
            }
        }
    }

    public class TrainingLocationScheduler : ITrainingScheduler {
        public void Schedule(TrainingDetail trainingDetails) {
            foreach (Training training in trainingDetails.TrainingDays) {
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
