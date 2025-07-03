namespace AdvancedUnitTesting.ResistanceToRefactoringExample.Exercise;

// Looking at the four pillars, is there a pillar which gets violated in this test?
public class Test2
{
    public void A_sandwich_can_be_made_based_on_its_recipe()
    {
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
