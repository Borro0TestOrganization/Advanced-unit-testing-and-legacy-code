// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.
namespace AdvancedUnitTesting.ResistanceToRefactoringExample;

public class SandwichRecipe
{
    public string Bread { get; set; } = "";
    public string Filling { get; set; } = "";
    public string Sauce { get; set; } = "";
}

public interface IMaker
{
    string Make(SandwichRecipe sandwichRecipe);
}

public class SandwichMaker : IMaker
{
    public IReadOnlyList<IMaker> IngredientMakers { get; }

    public SandwichMaker()
    {
        IngredientMakers = [
            new BreadMaker(),
            new FillingMaker(),
            new SauceMaker()
        ];
    }

    public string Make(SandwichRecipe sandwichRecipe)
    {
        string result = "";

        foreach (IMaker ingredientMaker in IngredientMakers)
        {
            result += ingredientMaker.Make(sandwichRecipe) + Environment.NewLine;
        }

        return result;
    }
}

public class BreadMaker : IMaker
{
    public string Make(SandwichRecipe sandwichRecipe)
    {
        return "Bread: " + sandwichRecipe.Bread;
    }
}

public class FillingMaker : IMaker
{
    public string Make(SandwichRecipe sandwichRecipe)
    {
        return "Filling: " + sandwichRecipe.Filling;
    }
}

public class SauceMaker : IMaker
{
    public string Make(SandwichRecipe sandwichRecipe)
    {
        return "Sauce: " + sandwichRecipe.Sauce;
    }
}
