namespace LegacyCode._2_ClassSprout.Example.Legacy {
    public class TrainingScheduler {
        public void CreateAgendaMeetings(TrainingDetail trainingDetails) {
            foreach (Training training in trainingDetails.TrainingDays) {
                AgendaMeeting agendaMeeting = new AgendaMeeting(
                    training.Day, training.StartsAt, training.EndsAt);

                agendaMeeting.SetLocation(training.Location);

                training.AddMeeting(agendaMeeting);
            }
        }
    }

    public class TrainingDetail {
        public IEnumerable<Training> TrainingDays { get; internal set; }

        public TrainingDetail(IEnumerable<Training> trainingDays) {
            TrainingDays = trainingDays;
        }
    }

    public class Training {
        public string Day { get; internal set; }
        public string StartsAt { get; internal set; }
        public string EndsAt { get; internal set; }
        public string Location { get; internal set; }
        public AgendaMeeting AgendaMeeting { get; internal set; }

        public Training(string day, string startsAt, string endsAt, string location) {
            Day = day;
            StartsAt = startsAt;
            EndsAt = endsAt;
            Location = location;
        }

        internal void AddMeeting(AgendaMeeting agendaMeeting) {
            AgendaMeeting = agendaMeeting;
        }
    }

    public class AgendaMeeting {
        public string Day { get; internal set; }
        public string StartsAt { get; internal set; }
        public string EndsAt { get; internal set; }
        public string Location { get; internal set; }

        public AgendaMeeting(string day, string startsAt, string endsAt) {
            Day = day;
            StartsAt = startsAt;
            EndsAt = endsAt;
        }

        internal void SetLocation(string location) {
            Location = location;
        }
    }
}
