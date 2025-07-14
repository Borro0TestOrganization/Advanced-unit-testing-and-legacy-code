// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.
using AdvancedUnitTesting.RefactoringTowardValuableTest.V4;

namespace AdvancedUnitTesting.RefactoringTowardValuableTest.ValuableTests;

public class TestDomainLogic
{
    // Valuable test
    public void Changing_from_fixed_budget_to_lease_car()
    {
        // Arrange
        int numberOfLeaseCars = 10;
        var altenLeaseCarFleet = new AltenLeaseCarFleet(numberOfLeaseCars);
        var lastTimeMobilityPlanChanged = new DateTime(2020, 1, 1);
        var consultant = new Consultant(
            123, MobilityPlan.FixedBudget, lastTimeMobilityPlanChanged);
        var now = new DateTime(2025, 1, 1);

        // Act
        consultant.ChangeMobilityPlan(MobilityPlan.LeaseCar, now, altenLeaseCarFleet);

        // Result
        Assert.That(consultant.MobilityPlan, Is.EqualTo(MobilityPlan.FixedBudget));
        Assert.That(altenLeaseCarFleet.NumberOfLeaseCars, Is.EqualTo(11));
    }
}
