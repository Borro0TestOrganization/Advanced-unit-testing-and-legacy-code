namespace LegacyCode._7_ProgrammingByDifference.Example._2_Anonymous {
    public class ProgrammingByDifference {
        [Test]
        public void testAnonymousFunEventScheduler() {
            // Arrange
            MailService mailService = new MailService();
            FunEventScheduler funEventScheduler = new AnonymousFunEventScheduler(mailService);

            FunEvent funEvent = new FunEvent(
                "Make Your Code Faster", "2025-07-24", "18:00", "20:00", "Apeldoorn");

            // Act
            funEventScheduler.SendInvititions(funEvent);

            // Assert
            Assert.That(mailService.Mails.Count, Is.EqualTo(1));
            Assert.That(mailService.Mails[0].Subject, Is.EqualTo(funEvent.Subject));
            Assert.That(mailService.Mails[0].Location, Is.EqualTo(funEvent.Location));
            Assert.That(mailService.Mails[0].Message, Is.EqualTo("Uncle Bob"));
            Assert.That(mailService.Mails[0].To, Is.Null);
            Assert.That(mailService.Mails[0].From, Is.EqualTo(string.Empty));
        }

        [Test]
        public void testFunEventScheduler() {
            // Arrange
            MailService mailService = new MailService();
            FunEventScheduler funEventScheduler = new FunEventScheduler(mailService);

            FunEvent funEvent = new FunEvent(
                "Make Your Code Faster", "2025-07-24", "18:00", "20:00", "Apeldoorn");

            // Act
            funEventScheduler.SendInvititions(funEvent);

            // Assert
            Assert.That(mailService.Mails.Count, Is.EqualTo(1));
            Assert.That(mailService.Mails[0].Subject, Is.EqualTo(funEvent.Subject));
            Assert.That(mailService.Mails[0].Location, Is.EqualTo(funEvent.Location));
            Assert.That(mailService.Mails[0].Message, Is.EqualTo("Uncle Bob"));
            Assert.That(mailService.Mails[0].To, Is.Null);
            Assert.That(mailService.Mails[0].From, Is.EqualTo("Make"));
        }
    }
}
