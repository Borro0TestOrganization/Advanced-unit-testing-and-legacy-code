// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.
namespace AdvancedUnitTesting.RefactoringTowardValuableTest.OverComplicated.ChangeClient;

public class Consultant
{
    public int ConsultantId { get; private set; }
    public ClientName Client { get; private set; }
    public UserType Type { get; private set; }
    public void ChangeClient(int userId, ClientName newClient)
    {
        object[] data = Database.GetUserById(userId);
        UserId = userId;
        Client = (ClientName)data[1];
        Type = (UserType)data[2];
        if (Client == newClient) return;
        object[] companyData = Database.GetCompany();
        string companyDomainName = (string)companyData[0];
        int numberOfEmployees = (int)companyData[1];
        string emailDomain = newClient.Split('@')[1];
        bool isEmailCorporate = emailDomain == companyDomainName;
        UserType newType = isEmailCorporate ? UserType.Employee : UserType.Customer;
        if (Type != newType)
        {
            int delta = newType == UserType.Employee ? 1 : -1;
            int newNumber = numberOfEmployees + delta;
            Database.SaveCompany(newNumber);
        }
        Client = newClient;
        Type = newType;
        Database.SaveUser(this);
        MessageBus.SendEmailChangedMessage(UserId, newClient);
    }
}

public enum ClientName
{
    Lely,
    Deltares,
    Nearfield
}
