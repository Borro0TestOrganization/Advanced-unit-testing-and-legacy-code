using System.Text;

namespace AdvancedUnitTesting.ResistanceToRefactoringExample.Exercise;

// Looking at the four pillars, is there a pillar which gets violated in this test?
public class Test3
{
    public void MessageRenderer_SourceCodeMatchesExpected()
    {
        var solutionRoot = Path.Combine(AppContext.BaseDirectory);

        // Construct the full path to the MessageRenderer.cs file
        var sourceFilePath = Path.Combine(solutionRoot, "AdvancedUnitTesting", "ResistanceToRefactoringExample", "SandwichExample.cs");

        string sourceCode = File.ReadAllText(sourceFilePath, Encoding.UTF8); // Or Encoding.Default
        string expectedSourceCode = """
namespace AdvancedUnitTesting.ResistanceToRefactoringExample;

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
""";
        Assert.That(expectedSourceCode, Is.EqualTo(sourceCode));
    }
}
