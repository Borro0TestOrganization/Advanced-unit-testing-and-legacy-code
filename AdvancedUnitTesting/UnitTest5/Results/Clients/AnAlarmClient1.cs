namespace AdvancedUnitTesting.UnitTest5.Results.Clients;

public class AnAlarmClient1
{
    // A class with the only goal of simulating a dependency on Alart
    // that has impact on the refactoring.
		
    public AnAlarmClient1()
    {
        Alarm anAlarm = new Alarm();
        anAlarm.Check();
        bool isAlarmOn = anAlarm.AlarmOn;
    }
}