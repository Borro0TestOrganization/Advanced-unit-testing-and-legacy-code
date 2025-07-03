// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.
namespace AdvancedUnitTesting.LeakingImplementationDetailsExample.Bad;

public class Consultant
{
    public string Name { get; set; }

    public string NormalizeName(string name)
    {
        string result = (name ?? "").Trim();
        if (result.Length > 50)
            return result.Substring(0, 50);
        return result;
    }
}

public class BusinessManager
{
    public void RenameConsultant(int consultantId, string newName)
    {
        Consultant consultant = GetConsultantFromDatabase(consultantId);
        string normalizedName = consultant.NormalizeName(newName);
        consultant.Name = normalizedName;
        SaveConsultantToDatabase(consultant);
    }


    // Do not copy below this
    Consultant GetConsultantFromDatabase(int id)
    {
        return new Consultant();
    }


    void SaveConsultantToDatabase(Consultant consultant)
    {
        // do nothing
    }
}

