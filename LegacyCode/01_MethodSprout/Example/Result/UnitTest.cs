namespace LegacyCode._1_MethodSprout.Example.Result {
    public class MethodSproutTests {
        [Test]
        public void OnlineTraining_ShouldSetMeetingURL() {
            // Arrange
            IList<Training> trainings = new List<Training>
            {
                new Training("2025-06-01", "9:30", "17:00", 
                    "https://microsoft.teams.com/a-very-fun-training", LocationType.Online) {
                    AgendaMeeting = new AgendaMeeting("2025-06-01", "9:30", "17:00")
                }
            };

            TrainingScheduler scheduler = new TrainingScheduler();

            // Act
            scheduler.SetMeetingLocations(trainings);

            // Assert
            Assert.NotNull(trainings[0].AgendaMeeting.MeetingURL);
            Assert.IsNull(trainings[0].AgendaMeeting.Location);
        }

        [Test]
        public void OfflineOrOnlineTraining_ShouldSetLocationOrURL() {
            // Arrange
            IList<Training> trainings = new List<Training>
            {
                new Training("2025-06-01", "9:30", "17:00", 
                    "https://microsoft.teams.com/a-very-fun-training", LocationType.Online) {
                    AgendaMeeting = new AgendaMeeting("2025-06-01", "9:30", "17:00")
                },
                new Training("2025-10-23", "8:30", "16:00", 
                    "Linie 544, Apeldoorn", LocationType.Offline) {
                    AgendaMeeting = new AgendaMeeting("2025-10-23", "8:30", "16:00")
                },
                new Training("2025-11-23", "8:30", "16:00",
                    "Linie 544, Apeldoorn", LocationType.Offline) {
                    AgendaMeeting = new AgendaMeeting("2025-11-23", "8:30", "16:00")
                }
            };

            TrainingScheduler scheduler = new TrainingScheduler();

            // Act
            scheduler.SetMeetingLocations(trainings);

            // Assert
            Assert.NotNull(trainings[0].AgendaMeeting.MeetingURL);
            Assert.IsNull(trainings[0].AgendaMeeting.Location);

            Assert.IsNull(trainings[1].AgendaMeeting.MeetingURL);
            Assert.NotNull(trainings[1].AgendaMeeting.Location);

            Assert.IsNull(trainings[2].AgendaMeeting.MeetingURL);
            Assert.NotNull(trainings[2].AgendaMeeting.Location);
        }
    }
}