using System;
using System.IO;

class Program {
    // Attributes:
    private static List<ADGoal> _ADGoalsList = new();
    private static List<ADGoal> _ADArchivedGoals = new();
    private static int _ADTotalPoints = 0;

    // Methods
    static int ADDisplayMenu() {
        List<string> ADMenuLines = new() {
            $"\nYou Have {_ADTotalPoints} points",
            "\nMENU OPTIONS:",
            "  1. Create New Goal",
            "  2. Display Goals",
            "  3. Record Goal Progress",
            "  4. Save Goals",
            "  5. Load Goals",
            "  6. Archive Goal",
            "  7. Display Archived Goals",
            "  8. Quit"
        };
        ADMenuLines.ForEach(Console.WriteLine);
        return ADIOUtility.ADGetValidInt("What would you like to do? ", 1, 8);
    }
    static ADGoal ADCreateGoalObject (string P_ClassName, List<object> P_Args) {
        Type ADType = Type.GetType(P_ClassName);
        object ADInstance = Activator.CreateInstance(ADType, P_Args.ToArray());
        return ADInstance as ADGoal;
    }
    // --------------- Menu Option 1 --------------- //
    static void ADCreateGoal() {
        // Get Goal Type
        List<string> ADGoalTypesMenu = new() {
            "Pick a goal type:",
            "  1. Simple",
            "  2. Eternal",
            "  3. Checklist"
        };
        ADGoalTypesMenu.ForEach(Console.WriteLine);
        int ADOption = ADIOUtility.ADGetValidInt("Select an option: ", 1, 3);

        List<object> ADGoalInfo = new();
        // Get Goal Info
        Console.Write("Write a description for this goal: ");
        ADGoalInfo.Add(Console.ReadLine());
        ADGoalInfo.Add(ADIOUtility.ADGetValidInt("How many points is recorded goal progress worth (Max 250)? ", 0, 250));

        // Create the goal
        switch (ADOption) {
            case 1:
                _ADGoalsList.Add(ADCreateGoalObject("ADSimple", ADGoalInfo));
                break;
            case 2:
                _ADGoalsList.Add(ADCreateGoalObject("ADEternal", ADGoalInfo));
                break;
            case 3:
                ADGoalInfo.Add(ADIOUtility.ADGetValidInt("How many bonus points is goal completion worth (Max 500)? ", 0, 500));
                ADGoalInfo.Add(ADIOUtility.ADGetValidInt("How steps to complete this goal (Max 20)? ", 0, 20));
                _ADGoalsList.Add(ADCreateGoalObject("ADChecklist", ADGoalInfo));
                break;
            default:
                Console.WriteLine("Error: Unknown goal option");
                break;
        }
    }
    // --------------- Menu Option 2/7 --------------- //
    static void ADDisplayGoals(List<ADGoal> P_GoalList) {
        if (P_GoalList.Count == 0) {
            Console.WriteLine("There are no goals to display");
        } else {
            P_GoalList.ForEach(goal => Console.WriteLine(goal.ADToString()));
        }
    }
    // --------------- Menu Option 3 --------------- //
    static ADGoal ADPickGoal() {
        if (_ADGoalsList.Count == 0) {
            Console.WriteLine("You haven't entered any goals.");
            return null;
        } else {
            Console.WriteLine("Goal list:");
            for (int i=0; i<_ADGoalsList.Count; i++) {
                Console.WriteLine($"  {i+1}. {_ADGoalsList[i].ADToEvent()}");
            }
            int ADGoalIndex = ADIOUtility.ADGetValidInt("Which goal do you choose? ", 1, _ADGoalsList.Count) - 1;
            return _ADGoalsList[ADGoalIndex];
        }
    }
    static void ADRecordGoalProgress() {
        ADGoal ADSelectedGoal = ADPickGoal();
        if (ADSelectedGoal != null) {
            _ADTotalPoints += ADSelectedGoal.ADMarkComplete();
        }
    }
    // --------------- Menu Option 4 --------------- //
    static void ADSaveGoals() {
        string ADFileName = ADIOUtility.ADGetFileName("What do you want to name your save file? ");
        using (StreamWriter ADGoalFile = new(ADFileName)) {
            ADGoalFile.WriteLine(_ADTotalPoints);
            _ADGoalsList.ForEach(goal => ADGoalFile.WriteLine($"Visible|{goal.ADToCSV()}"));
            _ADArchivedGoals.ForEach(goal => ADGoalFile.WriteLine($"Archived|{goal.ADToCSV()}"));
        }
        Console.WriteLine($"Goals saved in {ADFileName}");
    }
    // --------------- Menu Option 5 --------------- //
    static void ADLoadGoals() {
        try {
            string ADFileName = ADIOUtility.ADGetFileName("What file do you want to load from? ");
            string[] ADLines = File.ReadAllLines(ADFileName);
            bool ADFirstLine = true;
            foreach(string ADLine in ADLines) {
                if (ADFirstLine) {
                    _ADTotalPoints = int.Parse(ADLine);
                    ADFirstLine = false;
                } else {
                    string[] ADParts = ADLine.Split('|');
                    object[] ADArgs = ADParts[2].Split('~');
                    ADGoal LineGoal = ADCreateGoalObject(ADParts[1], ADIOUtility.ADUnparseArray(ADArgs).ToList());
                    if (ADParts[0] == "Archived") {
                        _ADArchivedGoals.Add(LineGoal);
                    } else {
                        _ADGoalsList.Add(LineGoal);
                    }
                }
            }
            Console.WriteLine($"Data successfully loaded from {ADFileName}");
        } catch {
            Console.WriteLine("The file doesn't exist or is missing");
        }
    }
    // --------------- Menu Option 6 --------------- //
    static void ADArchiveGoal() {
        ADGoal SelectedGoal = ADPickGoal();
        _ADArchivedGoals.Add(SelectedGoal);
        _ADGoalsList.Remove(SelectedGoal);
    }

    
    
    static void Main(string[] args) {
        int ADUserSelection;
        do {
            ADUserSelection = ADDisplayMenu();
            switch (ADUserSelection) {
                case 1:
                    ADCreateGoal();
                    break;
                case 2:
                    ADDisplayGoals(_ADGoalsList);
                    break;
                case 3:
                    ADRecordGoalProgress();
                    break;
                case 4:
                    ADSaveGoals();
                    break;
                case 5:
                    ADLoadGoals();
                    break;
                case 6:
                    ADArchiveGoal();
                    break;
                case 7:
                    ADDisplayGoals(_ADArchivedGoals);
                    break;
                case 8:
                    Console.WriteLine("Thank you for using the goals program");
                    break;
                default:
                    Console.WriteLine("Error: Unknown Menu Selection");
                    break;
            }
        } while (ADUserSelection != 8);
    }
}