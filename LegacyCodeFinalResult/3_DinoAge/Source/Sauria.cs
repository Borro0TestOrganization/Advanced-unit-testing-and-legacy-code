using System.Text;

namespace LegacyCodeFinalResult._3_DinoAge {
    internal class Sauria {
        private IRandomService _randomService;
        private IDictionary<string, (IList<Dinosaur> dinosaurs, decimal cost)> _sauria;

        public Sauria(IRandomService randomService) {
            _randomService = randomService;
            _sauria = new Dictionary<string, (IList<Dinosaur>, decimal)>();
        }

        public void AddDinosaur(string name, int amount, decimal cost) {
            IList<Dinosaur> dinosaurs;

            if (_sauria.ContainsKey(name)) {
                dinosaurs = _sauria[name].dinosaurs;
            } else {
                dinosaurs =  new List<Dinosaur>();
                _sauria.Add(name, (dinosaurs, cost));
            }

            for (int i = 0; i < amount; i++) {
                dinosaurs.Add(new Dinosaur(0));
            }
        }

        public void DinosaurAdded(string name) {
            _sauria[name].dinosaurs.Add(new Dinosaur(0));
        }

        public void DinosaurDied(string name) {
            _sauria[name].dinosaurs.RemoveAt(0);
        }

        public (string history, decimal runningCost) ProcessPeriod() {
            StringBuilder stringBuilder = new StringBuilder();
            decimal runningCosts = 0;

            foreach (KeyValuePair<string, (IList<Dinosaur> dinosaurs, decimal cost)> dinosaurGroup in _sauria) {
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("\tName:   " + dinosaurGroup.Key);
                stringBuilder.AppendLine("\tAmount: " + dinosaurGroup.Value.dinosaurs.Count);
                stringBuilder.AppendLine("\tCosts:  " + dinosaurGroup.Value.cost);

                stringBuilder.AppendLine("\tDinosaurs { ");
                foreach (Dinosaur dinosaur in dinosaurGroup.Value.dinosaurs) {
                    stringBuilder.AppendLine("\t\tAge:   " + dinosaur.Age);
                }
                stringBuilder.AppendLine("\t}");

                stringBuilder.AppendLine("}");

                runningCosts += dinosaurGroup.Value.dinosaurs.Count * dinosaurGroup.Value.cost;
            }


            return (stringBuilder.ToString(), runningCosts);
        }

        public string PickRandomDinosaurName() {
            return _sauria.Keys.ToArray()[_randomService.Next(0, _sauria.Keys.Count)];
        }

        public void NextYear() {
            foreach ((IList<Dinosaur> dinosaurs, decimal cost) dinoGroup in _sauria.Values) {
                foreach (Dinosaur dinosaur in dinoGroup.dinosaurs) {
                    dinosaur.BecomeOlder();
                }
            }
        }
    }
}
