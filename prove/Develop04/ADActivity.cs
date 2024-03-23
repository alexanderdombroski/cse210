using System;

class ADActivity {
    // Attributes:
    private readonly string _ADStartingMessage;
    private readonly string _ADEndingMessage;
    private int _ADDuration; // The duration of the pauses in the activity in miliseconds
    private readonly List<string> _ADPrompts;
    private List<int> _ADUnusedPromptIndexes = new();
    
    // Constructors:
    public ADActivity(string P_StartingMessage, string P_EndingMessage, List<string> P_Prompts) {
        _ADStartingMessage = P_StartingMessage;
        _ADEndingMessage = P_EndingMessage;
        _ADPrompts = P_Prompts;
        ADResetPrompts();
    }

    // Methods:
    private void ADResetPrompts() {
        _ADUnusedPromptIndexes = Enumerable.Range(0, _ADPrompts.Count).ToList();
    }
    public void ADSetDuration(int P_Duration) {
        _ADDuration = P_Duration;
    }
    public void ADPauseSeconds() {
        Thread.Sleep(_ADDuration);
    }
    public void StartActivity() {
        Console.WriteLine(_ADStartingMessage);
    }
    public void EndActivity() {
        Console.WriteLine(_ADEndingMessage);
    }
    public string GetRandomPrompt() {
        if (_ADUnusedPromptIndexes.Count == 0) {
            ADResetPrompts();
        }
        Random ADRndGen = new();
        int ADIndex = ADRndGen.Next(_ADUnusedPromptIndexes.Count);
        string ADPrompt = _ADPrompts[_ADUnusedPromptIndexes[ADIndex]];
        _ADUnusedPromptIndexes.RemoveAt(ADIndex);
        return ADPrompt;
    }

    public interface ADIRunnable {
        void ADRun();
    }
}