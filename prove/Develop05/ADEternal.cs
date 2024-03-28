public class ADEternal : ADGoal {
    // Attributes:
    private int _ADTimesCompleted = 0;

    // Constructors:
    public ADEternal(string P_Name, int P_Points) : base(P_Name, P_Points) {}
    public ADEternal(string P_Name, int P_Points, bool P_Completed, int P_TimesCompleted) : base(P_Name, P_Points, P_Completed) {
        _ADTimesCompleted = P_TimesCompleted;
    }

    // Methods:
    public override string ADToString() {
        return $"{ADToToDo()} -- completed {_ADTimesCompleted} times";
    }
    public override string ADToCSV() {
        return $"ADEternal|{ADToBaseCSV()}~{_ADTimesCompleted}";
    }
    public override int ADMarkComplete() {
        _ADTimesCompleted += 1;
        return _ADGoalPoints;
    }
}