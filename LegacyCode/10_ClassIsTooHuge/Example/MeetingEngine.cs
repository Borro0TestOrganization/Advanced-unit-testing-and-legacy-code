using System.Numerics;

namespace LegacyCode._10_ClassIsTooHuge.Example {
    public class MeetingPlan {
        private DateTime _startTime;
        private int _durationMinutes; 
        private decimal _roomHourlyRate;
        private List<Attendee> _attendees;
        private List<AgendaItem> _agendaItems;

        public MeetingPlan(DateTime startTime, int durationMinutes, decimal roomHourlyRate) {
            _startTime = startTime;
            _durationMinutes = durationMinutes;
            _roomHourlyRate = roomHourlyRate;

            _attendees = new List<Attendee>();
            _agendaItems = new List<AgendaItem>();
        }

        public void AddAttendee(Attendee attendee) {
            _attendees.Add(attendee);
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

        public double ComputeEngagementScore() {
            if (_attendees.Count == 0) {
                return 0;
            }

            int discussionMinutes = 0;
            foreach (AgendaItem agendaItem in _agendaItems) {
                if (agendaItem.RequiresDiscussion) {
                    discussionMinutes += agendaItem.EstimatedMinutes;
                }
            }

            return Math.Min(1.0, (double)discussionMinutes / Math.Max(1, _attendees.Count * 10));
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
