namespace LegacyCode._4_ClassWrap.Example.Result {
    public class ClassWrapTests {
        [Test]
        public void MeetingIsDuringWeekendSoOutsideBusinessHours() {
            // Arrange
            MeetingRequest weekend = new MeetingRequest {
                PreferredStart = new DateTime(2025, 8, 17, 10, 0, 0), // sunday
                Attendees = new List<Attendee> { new Attendee() { Email = "gerhard.kroes@alten.nl" } }
            };

            MeetingScheduler meetingScheduler = new MeetingScheduler();
            MeetingSchedulerBusinessHours wrapped = new MeetingSchedulerBusinessHours(meetingScheduler);

            // Act
            bool result = wrapped.Schedule(weekend);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void MeetingIsDuringEveningSoOutsideBusinessHours() {
            // Arrange
            MeetingRequest evening = new MeetingRequest {
                PreferredStart = new DateTime(2025, 8, 18, 18, 0, 0), // monday 18:00
                Attendees = new List<Attendee> { new Attendee() { Email = "gerhard.kroes@alten.nl" } }
            };

            MeetingScheduler meetingScheduler = new MeetingScheduler();
            MeetingSchedulerBusinessHours wrapped = new MeetingSchedulerBusinessHours(meetingScheduler);

            // Act
            bool result = wrapped.Schedule(evening);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void MeetingIsDuringWorkHoursSoOutsideBusinessHours() {
            // Arrange
            MeetingRequest evening = new MeetingRequest {
                PreferredStart = new DateTime(2025, 8, 18, 15, 0, 0), // monday 15:00
                Attendees = new List<Attendee> { new Attendee() { Email = "gerhard.kroes@alten.nl" } }
            };

            MeetingScheduler meetingScheduler = new MeetingScheduler();
            MeetingSchedulerBusinessHours wrapped = new MeetingSchedulerBusinessHours(meetingScheduler);

            // Act
            bool result = wrapped.Schedule(evening);

            // Assert
            Assert.True(result);
        }
    }
}