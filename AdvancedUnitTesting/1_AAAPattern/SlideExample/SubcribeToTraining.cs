// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.

namespace AdvancedUnitTesting.AAAPattern.SlideExample;

public class SubscribeToTraining
{
    public void Register_to_training_succeeds_when_enough_spots_available()
    {
        // Arrange
        var training = new AltenTraining("TDD training");
        training.SetAvailableSpots(10);
        var consultant = new Consultant();

        // Act
        bool success = consultant.RegisterToTraining(training, "boris");

        // Assert
        Assert.That(success, Is.True);
        Assert.That(training.GetAvailableSpots(), Is.EqualTo(9));
    }

    public void Register_to_training_succeeds_when_enough_spots_available_BAD()
    {
        // Arrange
        var training = new AltenTraining("TDD training");
        training.SetAvailableSpots(10);
        var consultant = new Consultant();

        // Act
        bool success = consultant.RegisterToTraining(training, "boris");
        training.RemoveAvailableSpot();

        // Assert
        Assert.That(success, Is.True);
        Assert.That(training.GetAvailableSpots(), Is.EqualTo(9));
    }
}

public class Consultant
{
    internal bool RegisterToTraining(AltenTraining training, string v)
    {
        training.RemoveAvailableSpot();
        return true;
    }
}

internal class AltenTraining
{
    private int _availableSpots;

    public AltenTraining()
    {
    }

    public AltenTraining(string _)
    {
    }

    internal int GetAvailableSpots()
    {
        return _availableSpots;
    }

    internal void RemoveAvailableSpot()
    {
        _availableSpots -= 1;
    }

    internal void SetAvailableSpots(int availableSpots)
    {
        _availableSpots = availableSpots;
    }
}
