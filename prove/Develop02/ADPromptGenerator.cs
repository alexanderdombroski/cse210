using System;

public class ADPromptGenerator {
    // Attributes
    readonly private List<string> _ADReferencePrompts = new List<string> {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What made me laugh today?",
        "What didn't go as planned today?",
        "What's an important lesson I learned today?"
    };
    public List<string> _ADPrompts = new();
    
    

    // Methods
    public ADPromptGenerator() {
        ADResetPrompts();
    }

    private void ADResetPrompts() {
       _ADPrompts = new List<string>(_ADReferencePrompts);
    }

    private string ADPop(List<string> p_Strings, int p_Index) {
        string ADReturnStr = p_Strings[p_Index];
        p_Strings.RemoveAt(p_Index);
        return ADReturnStr;
    }

    public string ADPickPrompt() {
        if (_ADPrompts.Count == 0) {
            ADResetPrompts();
        }
        Random ADRnd = new();
        int ADIndex = ADRnd.Next(_ADPrompts.Count);
        string ADPrompt = ADPop(_ADPrompts, ADIndex);
        return ADPrompt;
    }
}