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
        return ADGetValidInt("What would you like to do? ", 1, 8);
    }
    static int ADGetValidInt(string P_Prompt, int P_LowerBound, int P_UpperBound) {
        // Repeatedly asks user for a menu option choice until a valid answer is given
        bool ADInvalidResponse;
        int ADReturnValue;
        do {
            Console.Write(P_Prompt);
            string ADResponse = Console.ReadLine();
            ADInvalidResponse = !int.TryParse(ADResponse, out ADReturnValue);
        } while (ADReturnValue < P_LowerBound || ADReturnValue > P_UpperBound || ADInvalidResponse);
        return ADReturnValue;
    }
    static ADGoal ADCreateGoalObject (string P_ClassName, List<object> P_Args) {
        Type ADType = Type.GetType(P_ClassName);
        object ADInstance = Activator.CreateInstance(ADType, P_Args.ToArray());
        return ADInstance as ADGoal;
    }
    static void ADCreateGoal() {
        // Get Goal Type
        List<string> ADGoalTypesMenu = new() {
            "Pick a goal type:",
            "  1. Simple",
            "  2. Eternal",
            "  3. Checklist"
        };
        ADGoalTypesMenu.ForEach(Console.WriteLine);
        int ADOption = ADGetValidInt("Select an option: ", 1, 3);

        List<object> ADGoalInfo = new();
        // Get Goal Info
        Console.Write("Write a description for this goal: ");
        ADGoalInfo.Add(Console.ReadLine());
        ADGoalInfo.Add(ADGetValidInt("How many points is recorded goal progress worth (Max 250)? ", 0, 250));

        // Create the goal
        switch (ADOption) {
            case 1:
                _ADGoalsList.Add(ADCreateGoalObject("ADSimple", ADGoalInfo));
                break;
            case 2:
                _ADGoalsList.Add(ADCreateGoalObject("ADEternal", ADGoalInfo));
                break;
            case 3:
                ADGoalInfo.Add(ADGetValidInt("How many bonus points is goal completion worth (Max 500)? ", 0, 500));
                ADGoalInfo.Add(ADGetValidInt("How steps to complete this goal (Max 20)? ", 0, 20));
                _ADGoalsList.Add(ADCreateGoalObject("ADChecklist", ADGoalInfo));
                break;
            default:
                Console.WriteLine("Error: Uknown goal option");
                break;
        }
    }
    static void ADDisplayGoals(List<ADGoal> P_GoalList) {
        if (P_GoalList.Count == 0) {
            Console.WriteLine("There are no goals to display");
        } else {
            P_GoalList.ForEach(goal => Console.WriteLine(goal.ADToString()));
        }
    }
    static ADGoal ADPickGoal() {
        if (_ADGoalsList.Count == 0) {
            Console.WriteLine("You haven't entered any goals.");
            return null;
        } else {
            Console.WriteLine("Goal list:");
            for (int i=0; i<_ADGoalsList.Count; i++) {
                Console.WriteLine($"  {i+1}. {_ADGoalsList[i].ADToEvent()}");
            }
            int ADGoalIndex = ADGetValidInt("Which goal do you choose? ", 1, _ADGoalsList.Count) - 1;
            return _ADGoalsList[ADGoalIndex];
        }
    }
    static void ADRecordGoalProgress() {
        ADGoal ADSelectedGoal = ADPickGoal();
        if (ADSelectedGoal != null) {
            _ADTotalPoints += ADSelectedGoal.ADMarkComplete();
        }
    }
    static string ADGetFileName(string P_Prompt) {
        Console.Write(P_Prompt);
        string ADFileName = Console.ReadLine();
        return ADFileName.Contains('.') ? ADFileName : ADFileName + ".txt";
    }
    static void ADSaveGoals() {
        string ADFileName = ADGetFileName("What do you want to name your save file? ");
        using (StreamWriter ADGoalFile = new(ADFileName)) {
            ADGoalFile.WriteLine(_ADTotalPoints);
            _ADGoalsList.ForEach(goal => ADGoalFile.WriteLine($"Visible|{goal.ADToCSV()}"));
            _ADArchivedGoals.ForEach(goal => ADGoalFile.WriteLine($"Archived|{goal.ADToCSV()}"));
        }
        Console.WriteLine($"Goals saved in {ADFileName}");
    }
    static void ADLoadGoals() {
        try {
            string ADFileName = ADGetFileName("What file do you want to load from? ");
            string[] ADLines = File.ReadAllLines(ADFileName);
            bool ADFirstLine = true;
            foreach(string ADLine in ADLines) {
                if (ADFirstLine) {
                    _ADTotalPoints = int.Parse(ADLine);
                    ADFirstLine = false;
                } else {
                    string[] ADParts = ADLine.Split('|');
                    object[] ADArgs = ADParts[2].Split('~');
                    ADGoal LineGoal = ADCreateGoalObject(ADParts[1], ADUnparseArray(ADArgs).ToList());
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
    static object[] ADUnparseArray(object[] P_Array) {
        object[] ADReturnArray = new object[P_Array.Length];
        for(int i=0; i<P_Array.Length; i++) {
            if (int.TryParse(P_Array[i] as string, out int ADIntValue)) {
                ADReturnArray[i] = ADIntValue;
            } else if (bool.TryParse(P_Array[i] as string, out bool ADBoolValue)) {
                ADReturnArray[i] = ADBoolValue;
            } else {
                ADReturnArray[i] = P_Array[i];
            }
        }
        return ADReturnArray;
    }
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