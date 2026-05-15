
namespace AdvancedUnitTesting.FourPillarsTestEvaluation;

// Looking at the four pillars, is there a pillar which gets violated in this test?
public class Test3
{
    [Test]
    public void FunEventHost()
    {
        // Arrange
        var maxAmountOfParticipants = 4;
        var altenFunEvent = new AltenFunEvent(maxAmountOfParticipants);
        
        // Act
        var sut = new FunEventHost(altenFunEvent);

        // Assert
        Assert.That(sut, Is.Not.Null);
    }
}
