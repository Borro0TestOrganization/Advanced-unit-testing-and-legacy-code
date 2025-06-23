namespace AdvancedUnitTesting.ResistanceToRefactoringExample.Exercise;

// Looking at the four pillars, is there a pillar which gets violated in this test?
public class Test5
{
    public void Ingredient_makers()
    {
        var sut = new SandwichMaker();

        IReadOnlyList<IMaker> ingredientMakers = sut.IngredientMakers;

        Assert.That(sut, Is.Not.Null);
        Assert.That(ingredientMakers, Is.Not.Null);
    }
}
