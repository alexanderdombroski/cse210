using System;
class ADReflection : ADActivity, ADActivity.ADIRunnable {
    // Attributes:
    private readonly List<string> _ADQuestionList;
    private List<int> _ADUnusedQuestionIndexes;

    // Constructors:
    public ADReflection(string P_ActivityName, string P_Description, string P_EndingMessage, List<string> P_Prompts, List<string> P_Questions) : base(P_ActivityName, P_Description, P_EndingMessage, P_Prompts) {
        _ADQuestionList = P_Questions;
        ADResetQuestions();
    }
    
    // Methods:
    private void ADResetQuestions() {
        // Creates a list of indexes matching the length of _ADQuestionList
        _ADUnusedQuestionIndexes = Enumerable.Range(0, _ADQuestionList.Count).ToList();
    }
    private string ADGetRandomQuestion() {
        // Gets Random Question, no repeat
        if (_ADUnusedQuestionIndexes.Count == 0) {
            ADResetQuestions();
        }
        Random ADRndGen = new();
        int ADIndex = ADRndGen.Next(_ADUnusedQuestionIndexes.Count);
        string ADQuestion = _ADQuestionList[_ADUnusedQuestionIndexes[ADIndex]];
        _ADUnusedQuestionIndexes.RemoveAt(ADIndex);
        return ADQuestion;
    }

    private void ADDisplayInstruction() {
        // Display instructions
        Console.WriteLine("Consider the following prompt:\n");
        Console.WriteLine($"--- {ADGetRandomPrompt()} ---\n");
        Console.Write("When you have something in mind, press ENTER to continue. ");
        Console.ReadLine();
        Console.WriteLine("\nNow ponder on each of the following questions:");
        Console.Write("You may begin in: ");
        ADPauseMiliseconds(4000, new() {"4", "3", "2", "1"}, 1000);
        Console.Clear();
    }
    
    private void ADDisplayQuestion() {
        // Displays a random question and waits 7-8 seconds
        Console.Write($"> {ADGetRandomQuestion()} ");
        ADPauseMiliseconds(7500, new() {"?", "!", ":/", ")", "\b \b "}, 750);
    }

    public void ADRun() {
        // Run function
        ADStartActivity();
        ADDisplayInstruction();
        ADDoForDuration(ADDisplayQuestion);
        ADEndActivity();
    }
}