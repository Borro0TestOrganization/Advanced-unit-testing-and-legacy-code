// Change vscode color theme to Light+ to get a nice color scheme to copy into slides
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 13
namespace AdvancedUnitTesting.ResistanceToRefactoringExample;

public class SandwichMakerTestTwoExamples
{
    [Test]
    public void SandwichMaker_uses_right_ingredient_makers()
    {
        // Test coupled to implementation
        var sut = new SandwichMaker();

        IReadOnlyList<IMaker> ingredientMakers = sut.IngredientMakers;

        Assert.That(ingredientMakers, Has.Count.EqualTo(3));
        Assert.That(ingredientMakers[0], Is.InstanceOf<BreadMaker>());
        Assert.That(ingredientMakers[1], Is.InstanceOf<FillingMaker>());
        Assert.That(ingredientMakers[2], Is.InstanceOf<SauceMaker>());
    }

    [Test]
    public void A_sandwich_can_be_made_based_on_its_recipe()
    {
        // Test coupled to observable behavior
        var sut = new SandwichMaker();
        var sandwichRecipe = new SandwichRecipe
        {
            Bread = "White",
            Filling = "Ham",
            Sauce = "Truffle mayonnaise"
        };

        string sandwich = sut.Make(sandwichRecipe);

        Assert.That(sandwich, Is.EqualTo(
            """
            Bread: White
            Filling: Ham
            Sauce: Truffle mayonnaise

            """));
    }
}
