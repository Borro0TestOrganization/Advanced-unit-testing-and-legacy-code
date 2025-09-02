namespace LegacyCode._3_MethodWrap.Example.MidVersion {
    public class MethodWrapTests {
        [Test]
        public void TestScheduleFunMeeting() {
            // Arrange
            var meeting1 = new FunEvent(1, "New Meeting 1", "Description 1", "05:00");
            var meeting2 = new FunEvent(2, "New Meeting 2", "Description 2", "05:00");
            string location = "Room A";

            FunMeetingService funMeetingService = new FunMeetingService();

            // Act
            funMeetingService.ScheduleFunMeeting(meeting1, location);
            funMeetingService.ScheduleFunMeeting(meeting2, location);

            // Assert
            List<FunMeeting> scheduledMeetings = funMeetingService.GetScheduledMeetings();
            Assert.That(scheduledMeetings.Count, Is.EqualTo(1));
            Assert.That(scheduledMeetings[0].Title, Is.EqualTo("NEW MEETING 1"));
        }

        [Test]
        public void TestAddScheduleFunMeeting() {
            // Arrange
            var meeting1 = new FunEvent(1, "New Meeting 1", "Description 1", "05:00");
            var meeting2 = new FunEvent(2, "New Meeting 2", "Description 2", "05:00");
            string location = "Room A";

            FunMeetingService funMeetingService = new FunMeetingService();

            // Act
            funMeetingService.AddScheduleFunMeeting(meeting1, location);
            funMeetingService.AddScheduleFunMeeting(meeting2, location);

            // Assert
            List<FunMeeting> scheduledMeetings = funMeetingService.GetScheduledMeetings();
            Assert.That(scheduledMeetings.Count, Is.EqualTo(2));
            Assert.That(scheduledMeetings[0].Title, Is.EqualTo("NEW MEETING 1"));
            Assert.That(scheduledMeetings[1].Title, Is.EqualTo("NEW MEETING 2"));
        }
    }
}