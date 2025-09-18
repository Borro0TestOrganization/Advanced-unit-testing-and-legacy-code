namespace LegacyCode._08_TestHarness.Example._3_Fake {
    public class FunMeetingPlannerTests {
        [Test]
        public void TestTempatureBelow21GivesInsideMessage() {
            // Arrange
            FakeWeatherService weatherService = new FakeWeatherService(20, "Yes");
            FunMeetingPlanner funMeetingPlanner = new FunMeetingPlanner(weatherService);

            // Act
            string result = funMeetingPlanner.PlanMeeting();

            // Assert
            Assert.That(result, Is.EqualTo("Let's meet at the office"));
        }

        [Test]
        public void TestTempatureAbove20GivesOutsideMessage() {
            // Arrange
            IWeatherService weatherService = new FakeWeatherService(21, "Yes");
            FunMeetingPlanner funMeetingPlanner = new FunMeetingPlanner(weatherService);

            // Act
            string result = funMeetingPlanner.PlanMeeting();

            // Assert
            Assert.That(result, Is.EqualTo("Let's meet outside!"));
        }

        [Test]
        public void TestWithoutUsingFakeDependingOnHardware() {
            // Arrange
            IWeatherService weatherService = new WeatherService("Com1", 1000);
            FunMeetingPlanner funMeetingPlanner = new FunMeetingPlanner(weatherService);

            // Act
            FileNotFoundException fileNotFoundException = Assert.Throws<FileNotFoundException>(() => {
                funMeetingPlanner.PlanMeeting();
            });

            // Assert
            Assert.That(fileNotFoundException.Message, Does.Contain("Could not find file 'Com1'."));
        }
    }
}