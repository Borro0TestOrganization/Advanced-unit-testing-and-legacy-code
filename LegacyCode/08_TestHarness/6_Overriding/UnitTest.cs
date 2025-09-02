namespace LegacyCode._08_TestHarness._6_Overriding {
    public class FunMeetingPlannerTests {
        [Test]
        public void AddMeetingWillBeFirstSpot() {
            // Arrange
            MeetingRepository funMeetingPlannerRepository = new MeetingRepositoryStub();
            FunMeetingPlanner funMeetingPlanner = new FunMeetingPlanner(funMeetingPlannerRepository);
            funMeetingPlanner.AddFunMeeting("Monthly meeting");

            // Act
            string[] meetings = funMeetingPlanner.GetAllMeetings();

            // Assert
            Assert.That(meetings.Length, Is.EqualTo(1));
            Assert.That(meetings[0], Is.EqualTo("Monthly meeting"));
        }

        public class MeetingRepositoryStub : MeetingRepository {
            private List<string> meetings = [];

            public MeetingRepositoryStub() { }

            public override string[] GetAllMeetings() {
                return meetings.ToArray();
            }
            public override void AddFunMeeting(string meeting) {
                meetings.Add(meeting);
            }
        }
    }
}