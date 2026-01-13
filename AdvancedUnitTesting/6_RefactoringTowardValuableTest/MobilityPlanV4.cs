// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.
using System.Data;

namespace AdvancedUnitTesting.RefactoringTowardValuableTest.V4;

// Controller after refactoring
public class ConsultantController
{
    private readonly Database _database = new Database();
    private readonly MessageBus _messageBus = new MessageBus();

    public void ChangeMobilityPlan(int consultantId, MobilityPlan newMobilityPlan)
    {
        object[] consultantData = _database.GetConsultantById(consultantId);
        Consultant consultant = ConsultantFactory.Create(consultantData);

        object[] fleetData = _database.GetAltenFleetData();
        AltenLeaseCarFleet fleet = AltenLeaseCarFleetFactory.Create(fleetData);

        consultant.ChangeMobilityPlan(newMobilityPlan, DateTime.Now, fleet);

        _database.SaveAltenFleet(fleet);
        _database.SaveConsultant(consultant);
        _messageBus.SendMobilityPlanChangeRequestProcessed(consultantId);

    }
}

// Consultant after refactoring
public class Consultant
{
    private int id;

    public Consultant(int id, MobilityPlan mobilityPlan, DateTime dateLastMobilityPlanChange)
    {
        this.id = id;
        MobilityPlan = mobilityPlan;
        DateLastMobilityPlanChange = dateLastMobilityPlanChange;
    }

    public int ConsultantId { get; private set; }
    public MobilityPlan MobilityPlan { get; private set; }
    public DateTime DateLastMobilityPlanChange { get; private set; }

    public void ChangeMobilityPlan(
        MobilityPlan newMobilityPlan, DateTime now, AltenLeaseCarFleet fleet)
    {
        if (MobilityPlan == newMobilityPlan)
            return;

        TimeSpan timeSinceLastChange = now - DateLastMobilityPlanChange;
        if (fleet.IsAllowedToChangeFleet(now)
            && timeSinceLastChange > TimeSpan.FromDays(4 * 365))
        {
            DateLastMobilityPlanChange = now;
            MobilityPlan = newMobilityPlan;

            int delta = newMobilityPlan == MobilityPlan.LeaseCar ? 1 : -1;
            fleet.ChangeNumberOfLeaseCars(delta);
        }
    }
}

public enum MobilityPlan
{
    LeaseCar,
    FixedBudget
}


public class AltenLeaseCarFleet
{
    public AltenLeaseCarFleet(int numberOfLeaseCars)
    {
        NumberOfLeaseCars = numberOfLeaseCars;
    }

    public int NumberOfLeaseCars { get; private set; }

    public bool IsAllowedToChangeFleet(DateTime now)
    {
        return now.Month != 12;
    }

    public void ChangeNumberOfLeaseCars(int delta)
    {
        Precondition.Requires(NumberOfLeaseCars + delta >= 0);

        NumberOfLeaseCars += delta;
    }
}

public class Precondition
{
    internal static void Requires(bool v)
    {
        throw new NotImplementedException();
    }
}

public class AltenLeaseCarFleetFactory
{
    public static AltenLeaseCarFleet Create(object[] data)
    {
        int numberOfLeaseCars = (int)data[0];

        return new AltenLeaseCarFleet(numberOfLeaseCars);
    }
}

public class ConsultantFactory
{
    public static Consultant Create(object[] data)
    {
        int id = (int)data[0];
        MobilityPlan mobilityPlan = (MobilityPlan)data[1];
        DateTime dateLastMobilityPlanChange = (DateTime)data[2];

        return new Consultant(
            id, mobilityPlan, dateLastMobilityPlanChange);
    }
}


internal class MessageBus
{
    internal static void SendMobilityPlanChangedMessage(int consultantId)
    {
        throw new NotImplementedException();
    }

    internal static void SendNotAllowedMobilityPlanChangeMessage(int consultantId)
    {
        throw new NotImplementedException();
    }

    internal void SendMobilityPlanChangeRequestProcessed(int consultantId)
    {
        throw new NotImplementedException();
    }
}

class Database
{
    public object[] GetConsultantById(int consultantId)
    {
        return [];
    }

    public object[] GetAltenDivisionDataByClientEmailDomain(string clientEmailDomain)
    {
        return [];
    }

    public void SaveConsultant(Consultant consultant)
    {
    }

    internal int GetNumberOfLeaseCars()
    {
        throw new NotImplementedException();
    }

    internal void SaveNumberOfLeaseCars(int v)
    {
        throw new NotImplementedException();
    }

    internal object[] GetAltenFleetData()
    {
        throw new NotImplementedException();
    }

    internal void SaveAltenFleet(AltenLeaseCarFleet fleet)
    {
        throw new NotImplementedException();
    }
}

