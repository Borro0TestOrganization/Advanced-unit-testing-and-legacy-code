namespace LegacyCode._08_TestHarness.Example._6_Overriding {
    public class FunMeetingPlanner {
        private MeetingRepository _funMeetingPlannerRepository;

        public FunMeetingPlanner(MeetingRepository funMeetingPlannerRepository) {
            _funMeetingPlannerRepository = funMeetingPlannerRepository;
        }

        public string[] GetAllMeetings() {
            return _funMeetingPlannerRepository.GetAllMeetings();
        }

        public void AddFunMeeting(string meeting) {
            _funMeetingPlannerRepository.AddFunMeeting(meeting);
        }
    }

    public class MeetingRepository {
        private string dbFile = @"c:\temp\dataset.txt";

        public MeetingRepository() { }

        public virtual string[] GetAllMeetings() {
            return File.ReadAllLines(dbFile);
        }

        public virtual void AddFunMeeting(string meeting) {
            if (!File.Exists(dbFile)) {
                StreamWriter streamWriter = File.AppendText(dbFile);
                streamWriter.WriteLine(meeting);
            }
        }
    }
}
