// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.
using System.Data;

namespace AdvancedUnitTesting.RefactoringTowardValuableTest.OverComplicated.MobilityPlan.V1;

// Overcomplicated code
public class Consultant
{
    public int ConsultantId { get; private set; }
    public MobilityPlan MobilityPlan { get; private set; }
    public DateTime DateLastMobilityPlanChange { get; private set; }

    public void ChangeMobilityPlan(int consultantId, MobilityPlan newMobilityPlan)
    {
        object[] data = Database.GetConsultantById(consultantId);
        ConsultantId = consultantId;
        MobilityPlan = (MobilityPlan)data[1];
        DateLastMobilityPlanChange = (DateTime)data[2];

        if (MobilityPlan == newMobilityPlan) return;

        bool isDecember = DateTime.Now.Month == 12;

        TimeSpan timeSinceLastChange = DateTime.Now - DateLastMobilityPlanChange;
        if (!isDecember && timeSinceLastChange > TimeSpan.FromDays(4 * 365))
        {
            DateLastMobilityPlanChange = DateTime.Now;
            MobilityPlan = newMobilityPlan;

            int numberOfLeaseCars = Database.GetNumberOfLeaseCars();
            int delta = newMobilityPlan == MobilityPlan.LeaseCard ? 1 : -1;
            Database.StoreNumberOfLeaseCars(numberOfLeaseCars + delta);
            Database.SaveConsultant(this);
        }

        MessageBus.SendMobilityPlanChangeRequestProcessed(consultantId);
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

    internal static void SendMobilityPlanChangeRequestProcessed(int consultantId)
    {
        throw new NotImplementedException();
    }

    internal static void SendNotAllowedMobilityPlanChangeMessage(int consultantId)
    {
        throw new NotImplementedException();
    }
}

static class Database
{
    public static object[] GetConsultantById(int consultantId)
    {
        throw new NotImplementedException();
    }

    public static object[] GetAltenDivisionDataByClientEmailDomain(string clientEmailDomain)
    {
        throw new NotImplementedException();
    }

    public static void SaveConsultant(Consultant consultant)
    {
    }

    internal static int GetNumberOfLeaseCars()
    {
        throw new NotImplementedException();
    }

    internal static void StoreNumberOfLeaseCars(int v)
    {
        throw new NotImplementedException();
    }
}

