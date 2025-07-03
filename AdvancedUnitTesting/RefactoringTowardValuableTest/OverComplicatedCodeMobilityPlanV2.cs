// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.
using System.Data;

namespace AdvancedUnitTesting.RefactoringTowardValuableTest.OverComplicated.MobilityPlan.V2;

// Introduce controller class
public class ConsultantController
{
    private readonly Database _database = new Database();
    private readonly MessageBus _messageBus = new MessageBus();

    public void ChangeMobilityPlan(int consultantId, MobilityPlan newMobilityPlan)
    {
        object[] data = _database.GetConsultantById(consultantId);
        MobilityPlan mobilityPlan = (MobilityPlan)data[1];
        DateTime dateLastMobilityPlanChange = (DateTime)data[2];
        var consultant = new Consultant(
            consultantId, mobilityPlan, dateLastMobilityPlanChange);

        int numberOfLeaseCars = _database.GetNumberOfLeaseCars();

        int newNumberOfLeaseCars = consultant.ChangeMobilityPlan(
            newMobilityPlan, DateTime.Now, numberOfLeaseCars);

        _database.StoreNumberOfLeaseCars(newNumberOfLeaseCars);
        _database.SaveConsultant(consultant);
        _messageBus.SendMobilityPlanChangeRequestProcessed(consultantId);

    }
}

public class Consultant
{
    public Consultant(int consultantId, MobilityPlan mobilityPlan, DateTime dateLastMobilityPlanChange)
    {
        ConsultantId = consultantId;
        MobilityPlan = mobilityPlan;
        DateLastMobilityPlanChange = dateLastMobilityPlanChange;
    }

    public int ConsultantId { get; private set; }
    public MobilityPlan MobilityPlan { get; private set; }
    public DateTime DateLastMobilityPlanChange { get; private set; }

    // New version of ChangeMobilityPlan in Consultant Class
    public int ChangeMobilityPlan(
        MobilityPlan newMobilityPlan, DateTime now, int numberOfLeaseCars)
    {
        if (MobilityPlan == newMobilityPlan)
            return numberOfLeaseCars;

        TimeSpan timeSinceLastChange = now - DateLastMobilityPlanChange;
        if (timeSinceLastChange > TimeSpan.FromDays(4 * 365))
        {
            DateLastMobilityPlanChange = now;
            MobilityPlan = newMobilityPlan;

            int delta = newMobilityPlan == MobilityPlan.LeaseCard ? 1 : -1;
            numberOfLeaseCars += delta;
        }

        return numberOfLeaseCars;
    }
}

public enum MobilityPlan
{
    LeaseCard,
    FixedBudget
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

    internal void StoreNumberOfLeaseCars(int v)
    {
        throw new NotImplementedException();
    }
}

