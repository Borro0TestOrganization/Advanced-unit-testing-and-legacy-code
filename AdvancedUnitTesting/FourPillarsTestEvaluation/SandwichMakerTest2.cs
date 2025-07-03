namespace AdvancedUnitTesting.ResistanceToRefactoringExample.Exercise;

// Looking at the four pillars, is there a pillar which gets violated in this test?
public class Test4
{
    public void A_sandwich_can_be_made_based_on_its_recipe()
    {
        var sut = new SandwichMaker();
        var bread = File.ReadAllText("bread.txt");
        var filling = File.ReadAllText("filling.txt");
        var sauce = File.ReadAllText("sauce.txt");
        var sandwichRecipe = new SandwichRecipe
        {
            Bread = bread,
            Filling = filling,
            Sauce = sauce
        };

        string sandwich = sut.Make(sandwichRecipe);

        var expected_result = File.ReadAllText("result.txt");

        Assert.That(sandwich, Is.EqualTo(expected_result));
    }
}
