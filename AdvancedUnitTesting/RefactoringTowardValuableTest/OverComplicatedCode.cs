// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.
namespace AdvancedUnitTesting.RefactoringTowardValuableTest.OverComplicated;

public class Consultant
{
    public int ConsultantId { get; private set; }

    public string AltenEmail { get; private set; }
    public string ClientEmail { get; private set; }
    public AltenDivision AltenDivision { get; private set; }
    public void ChangeClientEmail(int consultantId, string newClientEmail)
    {
        object[] data = Database.GetConsultantById(consultantId);
        ConsultantId = consultantId;
        ClientEmail = (string)data[1];
        AltenDivision = (AltenDivision)data[2];
        if (ClientEmail == newClientEmail) return;
        string clientEmailDomain = newClientEmail.Split('@')[1];
        object[] newAltenDivisionData = Database.GetAltenDivisionDataByClientEmailDomain(clientEmailDomain);
        AltenDivision newAltenDivision = (AltenDivision)newAltenDivisionData[0];
        int newAltenDivisionNumberOfConsultants = (int)newAltenDivisionData[1];

        if (AltenDivision != newAltenDivision)
        {
            Database.StoreNumberOfConsultantsForDivision(newAltenDivision, newAltenDivisionNumberOfConsultants++);


            object[] oldAltenDivisionData = Database.GetAltenDivisionDataByClientEmailDomain(ClientEmail);
            int numberOfConsultants = (int)newAltenDivisionData[1];
            Database.RemoveConsultantFromAltenDivision(AltenDivision);
        }

        ClientEmail = newClientEmail;
        AltenDivision = newAltenDivision;
        Database.SaveConsultant(this);
        MessageBus.SendclientEmailChangedMessage(ConsultantId, newClientEmail);
    }
}

static class Database
{
    public static object[] GetConsultantById(int consultantId)
    {
        return [];
    }

    public static object[] GetAltenDivisionDataByClientEmailDomain(string clientEmailDomain)
    {
        return [];
    }

    public static void SaveConsultant(Consultant consultant)
    {
    }
}

public enum AltenDivision
{
    EnterpriseServicesApplicationsSoftware,
    Engineering,
    DataAi
}
