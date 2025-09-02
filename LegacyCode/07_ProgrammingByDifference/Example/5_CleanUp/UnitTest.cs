namespace LegacyCode._7_ProgrammingByDifference.Example._5_CleanUp {
    public class ProgrammingByDifference {
        [Test]
        public void testBlindCarbonCopyFunEventScheduler() {
            // Arrange
            MailService mailService = new MailService();
            Dictionary<string, string> configuration = new Dictionary<string, string>();
            configuration.Add("bcc", "true");

            FunEventScheduler funEventScheduler = new FunEventScheduler(mailService, configuration);
            FunEvent funEvent = CreateDefaultFunEvent();

            // Act
            funEventScheduler.SendInvititions(funEvent);

            // Assert
            Assert.That(mailService.Mails.Count, Is.EqualTo(1));
            Assert.That(mailService.Mails[0].Subject, Is.EqualTo(funEvent.Subject));
            Assert.That(mailService.Mails[0].Location, Is.EqualTo(funEvent.Location));
            Assert.That(mailService.Mails[0].Message, Is.EqualTo("Uncle Bob"));
            Assert.That(mailService.Mails[0].To, Is.Null);
            Assert.That(mailService.Mails[0].BlindCarbonCopy, Is.EqualTo("boris.blokland@alten.nl; gerhard.kroes@alten.nl; "));
            Assert.That(mailService.Mails[0].From, Is.EqualTo("MYCF"));
        }

        [Test]
        public void testFunEventSchedulerWithAnonymousConfiguration() {
            // Arrange
            MailService mailService = new MailService();
            Dictionary<string, string> configuration = new Dictionary<string, string>();
            configuration.Add("anonymous", "true");

            FunEventScheduler funEventScheduler = new FunEventScheduler(mailService, configuration);
            FunEvent funEvent = CreateDefaultFunEvent();

            // Act
            funEventScheduler.SendInvititions(funEvent);

            // Assert
            Assert.That(mailService.Mails.Count, Is.EqualTo(1));
            Assert.That(mailService.Mails[0].Subject, Is.EqualTo(funEvent.Subject));
            Assert.That(mailService.Mails[0].Location, Is.EqualTo(funEvent.Location));
            Assert.That(mailService.Mails[0].Message, Is.EqualTo("Uncle Bob"));
            Assert.That(mailService.Mails[0].To, Is.EqualTo("boris.blokland@alten.nl; gerhard.kroes@alten.nl; "));
            Assert.That(mailService.Mails[0].BlindCarbonCopy, Is.Null);
            Assert.That(mailService.Mails[0].From, Is.EqualTo(string.Empty));
        }

        [Test]
        public void testFunEventScheduler() {
            // Arrange
            MailService mailService = new MailService();
            Dictionary<string, string> configuration = new Dictionary<string, string>();

            FunEventScheduler funEventScheduler = new FunEventScheduler(mailService, configuration);
            FunEvent funEvent = CreateDefaultFunEvent();

            // Act
            funEventScheduler.SendInvititions(funEvent);

            // Assert
            Assert.That(mailService.Mails.Count, Is.EqualTo(1));
            Assert.That(mailService.Mails[0].Subject, Is.EqualTo(funEvent.Subject));
            Assert.That(mailService.Mails[0].Location, Is.EqualTo(funEvent.Location));
            Assert.That(mailService.Mails[0].Message, Is.EqualTo("Uncle Bob"));
            Assert.That(mailService.Mails[0].To, Is.EqualTo("boris.blokland@alten.nl; gerhard.kroes@alten.nl; "));
            Assert.That(mailService.Mails[0].BlindCarbonCopy, Is.Null);
            Assert.That(mailService.Mails[0].From, Is.EqualTo("MYCF"));
        }

        private FunEvent CreateDefaultFunEvent() {
            FunEvent funEvent = new FunEvent(
                "MYCF: Make Your Code Faster", "2025-07-24", "18:00", "20:00", "Apeldoorn");

            Participant boris = new Participant("boris.blokland@alten.nl", "Boris Blokland");
            Participant gerhard = new Participant("gerhard.kroes@alten.nl", "Gerhard Kroes");

            funEvent.AddParticipant(boris);
            funEvent.AddParticipant(gerhard);

            return funEvent;
        }
    }
}
