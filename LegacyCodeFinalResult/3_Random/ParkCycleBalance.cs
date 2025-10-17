using System.Text;

namespace LegacyCodeFinalResult._3_Random {
    public class ParkCycleBalance {
        private decimal _credit;
        private decimal _debit;

        public ParkCycleBalance() {
            _credit = 0;
            _debit = 0;
        }

        public void AddDebit(decimal debit) {
            _debit = debit;
        }

        public void AddCredit(decimal credit) {
            _credit = credit;
        }

        public void Reset() {
            _credit = 0;
            _debit = 0;
        }

        public decimal GetBalance() { 
            return _credit - _debit; 
        }

        public string Print() {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("-------Debit/Credit-------");
            stringBuilder.AppendLine("Credit:   " + _credit);
            stringBuilder.AppendLine("Debit :   " + _debit);

            return stringBuilder.ToString();
        }
    }
}
