using System;

class ADActivity {
    // Interfaces:
    public interface ADIRunnable {
        void ADRun();
    }

    // Attributes:
    private readonly string _ADActivityName;
    private readonly string _ADDescription;
    private readonly string _ADEndingMessage;
    private int _ADDuration; // The duration of the pauses in the activity in miliseconds
    private readonly List<string> _ADPrompts;
    private List<int> _ADUnusedPromptIndexes = new();
    
    // Constructors:
    public ADActivity(string P_ActivityName, string P_Description, string P_EndingMessage, List<string> P_Prompts) {
        _ADActivityName = P_ActivityName;
        _ADDescription = P_Description;
        _ADEndingMessage = P_EndingMessage;
        _ADPrompts = P_Prompts;
        ADResetPrompts();
    }

    // Methods:
    private void ADResetPrompts() {
        _ADUnusedPromptIndexes = Enumerable.Range(0, _ADPrompts.Count).ToList();
    }
    protected void ADPromptDurationChange() {
        Console.Write("How long, in seconds, would you like to do this activity for?: ");
        do {
            if (!int.TryParse(Console.ReadLine(), out _ADDuration)) {
                Console.Write("Please type a valid positive integer: ");
            }
        } while (_ADDuration == 0);
    }

    public static void ADPauseMiliseconds(int P_PauseTime, List<string> P_AnimationCharList = null, int AnimationFrameInterval = 250) {
        P_AnimationCharList ??= new() {"+", "*", "#"};
        int ADAnimationCount = P_PauseTime / AnimationFrameInterval;
        for (int i=0; i<ADAnimationCount; i++) {
            Console.Write(P_AnimationCharList[i % P_AnimationCharList.Count]);
            Thread.Sleep(AnimationFrameInterval);
            Console.Write("\b \b");
        }
        Console.WriteLine();
    }

    protected void ADStartActivity() {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_ADActivityName} Activity.\n");
        Console.WriteLine(_ADDescription + '\n');
        ADPromptDurationChange();
        Console.Clear();
        Console.Write("Get ready... ");
        ADPauseMiliseconds(2000);
        Console.WriteLine();
    }

    protected void ADEndActivity() {
        Console.Clear();
        Console.WriteLine(_ADEndingMessage + '\n');
        Console.Write("Press ENTER to return to the menu. ");
        Console.ReadLine();
    }
    protected string ADGetRandomPrompt() {
        if (_ADUnusedPromptIndexes.Count == 0) {
            ADResetPrompts();
        }
        Random ADRndGen = new();
        int ADIndex = ADRndGen.Next(_ADUnusedPromptIndexes.Count);
        string ADPrompt = _ADPrompts[_ADUnusedPromptIndexes[ADIndex]];
        _ADUnusedPromptIndexes.RemoveAt(ADIndex);
        return ADPrompt;
    }

    protected void ADDoForDuration(Action P_VoidFunction) {
        DateTime ADCurrentTime = DateTime.Now;
        DateTime ADEndTime = ADCurrentTime.AddSeconds(_ADDuration);
        do {
            P_VoidFunction();
            ADCurrentTime = DateTime.Now;
        } while(ADCurrentTime < ADEndTime);
    }
}