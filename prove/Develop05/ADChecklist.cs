class ADChecklist : ADGoal {
    // Attributes:
    private readonly int _ADBonusPoints;
    private readonly int _ADGoalSteps;
    private int _ADStepsCompleted = 0;

    // Constructors:
    public ADChecklist(string P_Name, int P_Points, int P_Bonus, int P_GoalSteps) : base(P_Name, P_Points) {
        _ADBonusPoints = P_Bonus;
        _ADGoalSteps = P_GoalSteps;
    }
    public ADChecklist(string P_Name, int P_Points, bool P_Completed, int P_Bonus, int P_GoalSteps, int P_StepsCompleted) : base(P_Name, P_Points, P_Completed) {
        _ADBonusPoints = P_Bonus;
        _ADGoalSteps = P_GoalSteps;
        _ADStepsCompleted = P_StepsCompleted;
    }
    
    // Methods:
    public override string ADToString() {
        return $"{ADToToDo()} -- {_ADStepsCompleted}/{_ADGoalSteps} steps completed";
    }
    public override string ADToCSV() {
        return $"ADChecklist|{ADToBaseCSV()}~{_ADBonusPoints}~{_ADGoalSteps}~{_ADStepsCompleted}";
    }
    public override int ADMarkComplete() {
        if (_ADGoalSteps - 1 > _ADStepsCompleted) {
            _ADStepsCompleted += 1;
            return _ADGoalPoints;
        } else if (!_ADGoalCompleted) {
            _ADGoalCompleted = true;
            _ADStepsCompleted += 1;
            return _ADGoalPoints + _ADBonusPoints;
        } else {
            Console.WriteLine("Goal Already Complete");
            return 0;
        }
    }
}