using LegacyCode._10_ClassIsTooHuge.Example;

namespace LegacyCode._10_ClassIsTooHuge.Splitting {
    public class AgendaPlanner {
        private DateTime _startTime;
        private int _durationMinutes;
        private decimal _roomHourlyRate;
        private List<AgendaItem> _agendaItems;

        public AgendaPlanner(DateTime startTime, int durationMinutes, decimal roomHourlyRate) {
            _startTime = startTime;
            _durationMinutes = durationMinutes;
            _roomHourlyRate = roomHourlyRate;

            _agendaItems = new List<AgendaItem>();
        }

        public void AddAgendaItem(AgendaItem item) {
            _agendaItems.Add(item);
        }

        public void ExtendMeeting(int extraMinutes) {
            _durationMinutes += extraMinutes;
        }

        public DateTime CalculateEndTime() {
            int totalEstimatedMinutes = 0;
            foreach (AgendaItem agendaItem in _agendaItems) {
                totalEstimatedMinutes += agendaItem.EstimatedMinutes;
            }

            int total = Math.Max(_durationMinutes, totalEstimatedMinutes);

            return _startTime.AddMinutes(total);
        }

        public decimal EstimateRoomCost() {
            int totalEstimatedMinutes = 0;
            foreach (AgendaItem agendaItem in _agendaItems) {
                totalEstimatedMinutes += agendaItem.EstimatedMinutes;
            }

            int total = Math.Max(_durationMinutes, totalEstimatedMinutes);
            double billableHours = Math.Ceiling(total / 60.0);

            return (decimal)billableHours * _roomHourlyRate;
        }
    }

    public sealed class AttendanceRoster {
        private List<Attendee> _attendees;
        public AttendanceRoster() {
            _attendees = new List<Attendee>();
        }

        public void AddAttendee(Attendee attendee) {
            _attendees.Add(attendee);
        }

        public bool IsOverCapacity(int maxAttendees) {
            return _attendees.Count > maxAttendees;
        }
    }

    public sealed class Attendee {
        public string Name { get; }
        public Attendee(string name) {
            Name = name;
        }
    }

    public sealed class AgendaItem {
        public string Title { get; }
        public int EstimatedMinutes { get; }
        public bool RequiresDiscussion { get; }

        public AgendaItem(string title, int estimatedMinutes, bool requiresDiscussion) {
            Title = title;
            EstimatedMinutes = estimatedMinutes;
            RequiresDiscussion = requiresDiscussion;
        }
    }
}
