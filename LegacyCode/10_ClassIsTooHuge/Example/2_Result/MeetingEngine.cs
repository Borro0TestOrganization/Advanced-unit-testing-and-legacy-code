namespace LegacyCode._10_ClassIsTooHuge.Example._2_Result {
    public class MeetingEngine {
        private Dictionary<string, object> _state;
        private RoomDirectory _rooms;
        private DecisionCalculator _decisionCalculator;
        private AgendaBuilder _agendaBuilder;
        private AttendeeRegisty _attendeeRegisty;
        private ConstraintValidator _constraintValidator;

        public MeetingEngine(RoomDirectory rooms) {
            _state = new Dictionary<string, object>();
            _rooms = rooms;
            _decisionCalculator = new DecisionCalculator();
            _agendaBuilder = new AgendaBuilder();
            _attendeeRegisty = new AttendeeRegisty();
            _constraintValidator = new ConstraintValidator();
        }

        public void PlanSchedule(string agendaText, Rules rules, Constraints constraints) {
            _constraintValidator.ValidateConstraints(constraints);

            Agenda agenda = _agendaBuilder.BuildAgenda(agendaText);
            IEnumerable<Topic> topics = _agendaBuilder.BuildTopics(agendaText);

            int requiredCapacity = _decisionCalculator.CalculatePreferredCapacity(topics, _attendeeRegisty.GetAttendees());
            Room suitableRoom = _rooms.FindRoomWithCapacity(requiredCapacity);

            if (suitableRoom == null) {
                throw new InvalidOperationException("No suitable room found with the required capacity.");
            }

            _constraintValidator.ValidateCapacity(suitableRoom, _attendeeRegisty.GetAttendees());

            Reservation reservation = _agendaBuilder.BuildReservation(suitableRoom, new TimeSpan(9, 0, 0), _attendeeRegisty.GetAttendees());
            IEnumerable<Decision> decisions = _decisionCalculator.CalculateDecisions(agenda, rules);

            _state["scheduledRoom"] = suitableRoom.Name;
            _state["reservation"] = reservation;
            _state["decisions"] = decisions.ToList();
        }
    }

    public class DecisionCalculator {
        public IEnumerable<Decision> CalculateDecisions(Agenda agenda, Rules rules) {
            return new List<Decision> {
                new Decision("Decision based on rules.")
            };
        }

        public int CalculatePreferredCapacity(IEnumerable<Topic> topics, IEnumerable<Attendee> attendees) {
            return (int)Math.Ceiling(attendees.Count() * 1.5);
        }
    }

    public class AgendaBuilder {
        public Agenda BuildAgenda(string agendaText) {
            return new Agenda("Meeting Agenda", agendaText);
        }

        public IEnumerable<Topic> BuildTopics(string agendaText) {
            return agendaText.Split(['\n'], StringSplitOptions.RemoveEmptyEntries)
                                   .Select(topic => new Topic(topic.Trim()))
                                   .ToList();
        }

        public Reservation BuildReservation(Room room, TimeSpan slot, IEnumerable<Attendee> attendees) {
            return new Reservation(room, DateTime.Today + slot, attendees.ToList());
        }
    }

    public class AttendeeRegisty {
        private List<Attendee> _attendees;

        public AttendeeRegisty() {
            _attendees = new List<Attendee>();
        }

        public List<Attendee> GetAttendees() {
            return _attendees;
        }

        public void RegisterAttendee(Attendee attendee) {
            if (!_attendees.Contains(attendee)) {
                _attendees.Add(attendee);
            }
        }

        public int GetRegisteredCount() {
            return _attendees.Count;
        }
    }

    public class ConstraintValidator {
        public void ValidateConstraints(Constraints constraints) {
            if (constraints.MeetingDate < DateTime.Now.Date) {
                throw new ArgumentException("Meeting date cannot be in the past.");
            }
        }

        public void ValidateCapacity(Room room, IEnumerable<Attendee> attendees) {
            if (attendees.Count() > room.Capacity) {
                throw new InvalidOperationException("The number of attendees exceeds the room's capacity.");
            }
        }
    }

    public class Rules { }

    public class Constraints {
        public DateTime MeetingDate { get; }

        public Constraints(DateTime dateTime) { 
            MeetingDate = dateTime; 
        }
    }

    public class Attendee {
        public string Name { get; }

        public Attendee(string name) {
            Name = name;
        }
    }

    public class Room {
        public string Name { get; }
        public int Capacity { get; }

        public Room(string name, int capacity) {
            Name = name;
            Capacity = capacity;
        }
    }

    public class RoomDirectory {
        private List<Room> _rooms;

        public RoomDirectory() {
            _rooms = new List<Room>();

            _rooms.Add(new Room("Switzerland", 6));
            _rooms.Add(new Room("Italy", 10));
        }

        public Room FindRoomWithCapacity(int minCapacity) {
            foreach (Room room in _rooms) {
               if (room.Capacity >= minCapacity) {
                    return room;
                }
            }

            return null;
        }
    }

    public class Agenda {
        public string Title { get; }
        public string Description { get; }

        public Agenda(string title, string description) {
            Title = title;
            Description = description;
        }
    }

    public class Topic {
        public string Title { get; }

        public Topic(string title) {
            Title = title;
        }
    }

    public class Reservation {
        public Room Room { get; }
        public DateTime StartTime { get; }
        public List<Attendee> Attendees { get; }

        public Reservation(Room room, DateTime startTime, List<Attendee> attendees) {
            Room = room;
            StartTime = startTime;
            Attendees = attendees;
        }
    }

    public class Decision {
        public string Text { get; set; }

        public Decision(string text) {
            Text = text;
        }
    }
}
