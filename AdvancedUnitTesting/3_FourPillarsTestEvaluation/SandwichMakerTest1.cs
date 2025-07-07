namespace AdvancedUnitTesting.ResistanceToRefactoringExample.Exercise;

// Looking at the four pillars, is there a pillar which gets violated in this test?
public class Test1
{
    public void SandwichMaker_uses_right_ingredient_makers()
    {
        var sut = new SandwichMaker();

        IReadOnlyList<IMaker> ingredientMakers = sut.IngredientMakers;

        Assert.That(ingredientMakers, Has.Count.EqualTo(3));
        Assert.That(ingredientMakers[0], Is.InstanceOf<BreadMaker>());
        Assert.That(ingredientMakers[1], Is.InstanceOf<FillingMaker>());
        Assert.That(ingredientMakers[2], Is.InstanceOf<SauceMaker>());
    }
}
