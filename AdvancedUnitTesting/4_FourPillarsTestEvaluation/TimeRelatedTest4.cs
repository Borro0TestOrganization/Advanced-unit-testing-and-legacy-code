
using System.Diagnostics;

namespace AdvancedUnitTesting.FourPillarsTestEvaluation;

// Looking at the four pillars, is there a pillar which gets violated in this test?
public class TimeRelatedTest4
{
    [Test]
    public void After_delay_action_is_executed()
    {
        // Arrange
        var worker = new Worker();
        var stopwatch = Stopwatch.StartNew();

        // Act
        worker.DoWorkAfterDelay(500); 
        stopwatch.Stop();

        // Assert
        Assert.That(worker.HasExecuted, Is.True);
        Assert.That(stopwatch.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(500));
    }
}

public class Worker
{
    private readonly Action<int> _sleepAction;
    public bool HasExecuted { get; private set; } = false;

    public Worker() : this(ms => System.Threading.Thread.Sleep(ms)) 
    {
    }
    public Worker(Action<int> sleepAction)
    {
        _sleepAction = sleepAction;
    }

    public void DoWorkAfterDelay(int milliseconds)
    {
        _sleepAction(milliseconds);
        HasExecuted = true;
    }
}