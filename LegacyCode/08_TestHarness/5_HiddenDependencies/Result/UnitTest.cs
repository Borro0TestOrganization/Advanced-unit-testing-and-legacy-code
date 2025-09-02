namespace LegacyCode._08_TestHarness.Result._5_HiddenDependencies {
    public class FunMeetingPlannerTests {
        [Test]
        public void PlanMeetingOnSundayShowDontMeet() {
            // Arrange
            FakeWeatherService weatherService = new FakeWeatherService(20, "Yes");
            IDateTimeService dateTimeService = new FakeDateTimeService(new DateTime(2021, 1, 17)); //2021-01-17 is a sunday
            FunMeetingPlanner funMeetingPlanner = new FunMeetingPlanner(weatherService, dateTimeService);

            // Act
            string result = funMeetingPlanner.PlanMeeting();

            // Assert
            Assert.That(result, Is.EqualTo("Sunday's we don't meet!"));
        }

        [Test]
        public void TestTempatureBelow21GivesInsideMessage() {
            // Arrange
            FakeWeatherService weatherService = new FakeWeatherService(20, "Yes");
            IDateTimeService dateTimeService = new FakeDateTimeService(new DateTime(2021, 1, 15));
            FunMeetingPlanner funMeetingPlanner = new FunMeetingPlanner(weatherService, dateTimeService);

            // Act
            string result = funMeetingPlanner.PlanMeeting();

            // Assert
            Assert.That(result, Is.EqualTo("Let's meet at the office"));
        }

        [Test]
        public void TestTempatureAbove20GivesOutsideMessage() {
            // Arrange
            IWeatherService weatherService = new FakeWeatherService(21, "Yes");
            IDateTimeService dateTimeService = new FakeDateTimeService(new DateTime(2021, 1, 15));
            FunMeetingPlanner funMeetingPlanner = new FunMeetingPlanner(weatherService, dateTimeService);

            // Act
            string result = funMeetingPlanner.PlanMeeting();

            // Assert
            Assert.That(result, Is.EqualTo("Let's meet outside!"));
        }

        [Test]
        public void TestWithoutUsingFakeDependingOnHardware() {
            // Arrange
            IWeatherService weatherService = new WeatherService("Com1", 1000);
            IDateTimeService dateTimeService = new FakeDateTimeService(new DateTime(2021, 1, 15));
            FunMeetingPlanner funMeetingPlanner = new FunMeetingPlanner(weatherService, dateTimeService);

            // Act
            FileNotFoundException fileNotFoundException = Assert.Throws<FileNotFoundException>(() => {
                funMeetingPlanner.PlanMeeting();
            });

            // Assert
            Assert.That(fileNotFoundException.Message, Does.Contain("Could not find file 'Com1'."));
        }
    }
}