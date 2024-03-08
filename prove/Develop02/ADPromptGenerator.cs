// Alex Dombroski - 03/08/2024
/* Creativity - Pre-set Journal Prompts are randomly generated 
and removed when used. When every prompt has been used, 
the list resets so every prompt can be used again. */

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
        // Constructor Function
        ADResetPrompts();
    }

    private void ADResetPrompts() {
        // CREATIVITY - Resets the modifiable prompt list
       _ADPrompts = new List<string>(_ADReferencePrompts);
    }

    private static string ADPop(List<string> p_Strings, int p_Index) {
        // Returns and removes a value of a string list via index
        string ADReturnStr = p_Strings[p_Index];
        p_Strings.RemoveAt(p_Index);
        return ADReturnStr;
    }

    public string ADPickPrompt() {
        // CREATIVITY - Resets prompts if neccessary
        if (_ADPrompts.Count == 0) {
            ADResetPrompts();
        }

        // Returns and removes random prompt
        Random ADRnd = new();
        int ADIndex = ADRnd.Next(_ADPrompts.Count);
        string ADPrompt = ADPop(_ADPrompts, ADIndex);
        return ADPrompt;
    }
}