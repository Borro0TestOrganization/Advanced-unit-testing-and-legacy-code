// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.
// Optional: In home -> Paragraph -> Line Spacing: Adjust to desired value
namespace AdvancedUnitTesting.LeakingImplementationDetailsExample.Good;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class Consultant
{
    private string _name;

    public string Name
    {
        get => _name;
        set => _name = NormalizeName(value);
    }


    private string NormalizeName(string name)
    {
        string result = (name ?? "").Trim();
        if (result.Length > 50)
            return result.Substring(0, 50);
        return result;
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class BusinessManager
{
    public void RenameConsultant(int consultantId, string newName)
    {
        Consultant consultant = GetConsultantFromDatabase(consultantId);
        consultant.Name = newName;
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

