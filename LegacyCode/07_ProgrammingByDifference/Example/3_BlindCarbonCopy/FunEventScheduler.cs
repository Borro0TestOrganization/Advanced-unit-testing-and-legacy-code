using System.Text.RegularExpressions;

namespace LegacyCode._7_ProgrammingByDifference.Example._3_BlindCarbonCopy {
    public class FunEventScheduler {
        protected readonly MailService _mailService;

        public FunEventScheduler(MailService mailService) {
            _mailService = mailService;
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

        protected virtual string GetFrom(FunEvent funEvent) {
            string from = funEvent.Subject;

            if (from != null && from.Length > 0)
                return Regex.Replace(from.Split()[0], @"[^0-9a-zA-Z\ ]+", "");

            return GetDefaultFrom();
        }

        protected virtual void ProcessParticipants(FunEvent funEvent, Mail mail) {
            funEvent.participants.ForEach(participant => {
                mail.To += participant.Email;
                mail.To += "; ";
            });
        }

        protected string GetDefaultFrom() {
            return "ALTEN FUN EVENTS";
        }
    }

    public class AnonymousFunEventScheduler : FunEventScheduler {
        public AnonymousFunEventScheduler(MailService mailService) : base(mailService) {
        }

        override
        protected string GetFrom(FunEvent funEvent) {
            return string.Empty;
        }
    }

    public class BlindCarbonCopyFunEventScheduler : FunEventScheduler {
        public BlindCarbonCopyFunEventScheduler(MailService mailService) : base(mailService) {
        }

        override
        protected void ProcessParticipants(FunEvent funEvent, Mail mail) {
            funEvent.participants.ForEach(participant => {
                mail.BlindCarbonCopy += participant.Email;
                mail.BlindCarbonCopy += "; ";
            });
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
