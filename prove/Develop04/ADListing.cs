using System;

class ADListing : ADActivity, ADActivity.ADIRunnable {
    // Attributes
    private int _ADAnswerCount = 0;

    // Constructors:
    public ADListing(string P_ActivityName, string P_Description, string P_EndingMessage, List<string> P_Prompts) : base(P_ActivityName, P_Description, P_EndingMessage, P_Prompts) {}

    private void ADDisplayInstruction() {
        // Displays Activity related instructions, and gives time to read them
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine($"--- {ADGetRandomPrompt()} ---");
        Console.Write("You may begin in: ");
        ADPauseMiliseconds(4000, new() {"4", "3", "2", "1"}, 1000);
    }
    private void ADGetAnswer() {
        // Gets a single answer, and increments answer count
        Console.Write("> ");
        Console.ReadLine();
        _ADAnswerCount += 1;
    }

    private void ADDisplayScore() {
        // Displays answer cound
        Console.Write($"You listed {_ADAnswerCount} items! Well done !! ");
        ADPauseMiliseconds(6000);
    }

    // Methods:
    public void ADRun() {
        // Run function
        ADStartActivity();
        ADDisplayInstruction();
        ADDoForDuration(ADGetAnswer);
        ADDisplayScore();
        ADEndActivity();
    }
}
