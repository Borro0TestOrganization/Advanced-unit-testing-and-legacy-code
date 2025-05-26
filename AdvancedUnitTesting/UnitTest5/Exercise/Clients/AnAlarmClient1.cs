namespace AdvancedUnitTesting.UnitTest5.Exercise.Clients;

public class AnAlarmClient1
{
    // A class with the only goal of simulating a dependency on Alert
    // that has impact on the refactoring.
		
    public AnAlarmClient1()
    {
        Alarm anAlarm = new Alarm();
        anAlarm.Check();
        bool isAlarmOn = anAlarm.AlarmOn;
    }
}