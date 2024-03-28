public class ADSimple : ADGoal {
    // Constructors:
    public ADSimple(string P_Name, int P_Points) : base(P_Name, P_Points) {}
    public ADSimple(string P_Name, int P_Points, bool P_Completed) : base(P_Name, P_Points, P_Completed) {}

    // Methods:
    public override string ADToString() {
        return ADToToDo();
    }
    public override string ADToCSV() {
        return $"ADSimple|{ADToBaseCSV()}";
    }
    public override int ADMarkComplete() {
        if (_ADGoalCompleted) {
            Console.WriteLine("Goal Already Complete");
            return 0;
        } else {
            _ADGoalCompleted = true;
            return _ADGoalPoints;
        }
    }
}