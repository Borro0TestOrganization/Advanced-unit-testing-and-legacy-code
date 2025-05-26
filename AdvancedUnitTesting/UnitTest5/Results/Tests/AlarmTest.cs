namespace AdvancedUnitTesting.UnitTest5.Results.Tests;

[TestFixture]
public class AlarmTest
{
    [Test]
    public void a_normal_pressure_value_should_not_raise_the_alarm()
    {
        var stubSensor = new StubSensor();
        stubSensor.StubCallToPopNextPressurePsiValue(Alarm.LowPressureThreshold);
        var target = new Alarm(stubSensor);

        target.Check();

        Assert.That(target.AlarmOn, Is.False);
    }

    [Test]
    public void a_pressure_value_out_of_range_should_raise_the_alarm()
    {
        var stubSensor = new StubSensor();
        stubSensor.StubCallToPopNextPressurePsiValue(Alarm.LowPressureThreshold -1);
        var target = new Alarm(stubSensor);

        target.Check();

        Assert.That(target.AlarmOn, Is.True);
    }

    [Test]
    public void a_normal_pressure_value_after_an_out_of_range_pressure_value_should_keep_the_alarm_on()
    {
        var stubSensor = new StubSensor();
        stubSensor.StubCallToPopNextPressurePsiValues(new[] { Alarm.LowPressureThreshold, Alarm.LowPressureThreshold - 1, Alarm.LowPressureThreshold });
        var target = new Alarm(stubSensor);

        target.Check();
        target.Check();
        target.Check();

        Assert.That(target.AlarmOn, Is.True);
    }
}