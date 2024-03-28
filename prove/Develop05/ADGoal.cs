public abstract class ADGoal {
    // Attributes:
    protected readonly string _ADGoalName;
    protected readonly int _ADGoalPoints;
    protected bool _ADGoalCompleted = false;
    
    // Constructors:
    public ADGoal(string P_Name, int P_Points) {
        // Constructor used for creating new goals
        _ADGoalName = P_Name;
        _ADGoalPoints = P_Points;
    }
    public ADGoal(string P_Name, int P_Points, bool P_Completed) {
        // Constructor used for loading goals in from files
        _ADGoalName = P_Name;
        _ADGoalPoints = P_Points;
        _ADGoalCompleted = P_Completed;
    }

    // Methods:
    protected string ADToBaseCSV() {
        return $"{_ADGoalName}~{_ADGoalPoints}~{_ADGoalCompleted}";
    }
    protected string ADToToDo() {
        return $"[{(_ADGoalCompleted ? "X" : " ")}] {_ADGoalName}";
    }
    public abstract string ADToString();
    public abstract string ADToCSV();
    public string ADToEvent() {
        return _ADGoalName;
    }
    public abstract int ADMarkComplete();
}