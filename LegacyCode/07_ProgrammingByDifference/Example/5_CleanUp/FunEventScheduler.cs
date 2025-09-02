using System.Text.RegularExpressions;

namespace LegacyCode._7_ProgrammingByDifference.Example._5_CleanUp {
    public class FunEventScheduler {
        private readonly MailService _mailService;
        private readonly Dictionary<string, string> _configuration;

        public FunEventScheduler(
            MailService mailService, 
            Dictionary<string, string> configuration
            ) {
            _mailService = mailService;
            _configuration = configuration;
        }

        public void SendInvitations(FunEvent funEvent) {
            Mail mail = new Mail();

            mail.Subject = funEvent.Subject;
            mail.Location = funEvent.Location;
            mail.From = GetFrom(funEvent);
            mail.Message = "Uncle Bob";

            ProcessParticipants(funEvent, mail);

            _mailService.Send(mail);
        }

        private string GetFrom(FunEvent funEvent) {
            string form;

            if (IsAnonymousEnabled()) {
                form = GetAnonymousFrom();
            } else {
                form = GetFromFunEvent(funEvent);
            }

            return form;
        }

        private string GetAnonymousFrom() {
            return string.Empty;
        }

        private string GetFromFunEvent(FunEvent funEvent) {
            string from = funEvent.Subject;

            if (from != null && from.Length > 0)
                return Regex.Replace(from.Split()[0], @"[^0-9a-zA-Z\ ]+", "");

            return GetDefaultFrom();
        }

        private bool IsAnonymousEnabled() {
            return _configuration.ContainsKey("anonymous") &&
                _configuration["anonymous"].Equals("true");
        }

        private bool IsBlindCarbonCopyEnabled() {
            return _configuration.ContainsKey("bcc") &&
                _configuration["bcc"].Equals("true");
        }

        private void ProcessParticipants(FunEvent funEvent, Mail mail) {
            funEvent.participants.ForEach(participant => {
                if (IsBlindCarbonCopyEnabled()) {
                    AddParticipantAsBlindCarbonCopy(mail, participant);
                } else {
                    AddParticipantAsTo(mail, participant);
                }
            });
        }

        private void AddParticipantAsBlindCarbonCopy(Mail mail, Participant participant) {
            mail.BlindCarbonCopy += participant.Email;
            mail.BlindCarbonCopy += "; ";
        }

        private void AddParticipantAsTo(Mail mail, Participant participant) {
            mail.To += participant.Email;
            mail.To += "; ";
        }

        private string GetDefaultFrom() {
            return "ALTEN FUN EVENTS";
        }
    }

    public class FunEvent {
        public string Subject { get; internal set; }
        public string Day { get; internal set; }
        public string StartsAt { get; internal set; }
        public string EndsAt { get; internal set; }
        public string Location { get; internal set; }
        public List<Participant> participants { get; internal set; }

        public FunEvent(string subject, string day, string startsAt, string endsAt, string location) {
            Subject = subject;
            Day = day;
            StartsAt = startsAt;
            EndsAt = endsAt;
            Location = location;
            participants = new List<Participant>();
        }

        public void AddParticipant(Participant participant) {
            participants.Add(participant);
        }
    }

    public class Participant {
        public string Email { get; internal set; }
        public string Name { get; internal set; }

        public Participant(string email, string name) {
            Email = email;
            Name = name;
        }
    }

    public class MailService {
        public List<Mail> Mails { get; internal set; }

        public MailService() {
            Mails = new List<Mail>();
        }

        public void Send(Mail mail) {
            Mails.Add(mail);
        }
    }

    public class Mail {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string CarbonCopy { get; set; }
        public string BlindCarbonCopy { get; set; }
        public string Location { get; set; }
    }
}
