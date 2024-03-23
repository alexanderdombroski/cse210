// CREATIVITY - Added a graditude activity

using System;

class ADGratitude : ADActivity, ADActivity.ADIRunnable {
    // Attributes:
    int _ADItemsListed = 0;

    // Constructors:
    public ADGratitude(string P_ActivityName, string P_Description, string P_EndingMessage, List<string> P_Prompts) : base(P_ActivityName, P_Description, P_EndingMessage, P_Prompts) {}

    // Methods:
    private void ADMakeGratitudeList() {
        // Displays a gratitude prompt and the user must write things they're grateful for for 15 seconds
        Console.WriteLine(ADGetRandomPrompt());
        ADDoForDuration(ADGetGratitudeItem, 15);
    }

    private void ADDisplayInstruction() {
        // Displays gratitude activity instructions
        Console.Write("List answers you are grateful for in the following categories: ");
        ADPauseMiliseconds(4000, new() {"4", "3", "2", "1"}, 1000);
    }

    private void ADGetGratitudeItem() {
        // Gets a single item listed, and increments the running total
        Console.Write("> ");
        Console.ReadLine();
        _ADItemsListed += 1;
    }

    private void ADDisplayScore() {
        // Displays the amount of items listed and gives time to read it
        Console.Write($"You listed {_ADItemsListed} items! Well done !! ");
        ADPauseMiliseconds(6000);
    }

    public void ADRun() {
        // Runs the activity
        ADStartActivity();
        ADDisplayInstruction();
        ADDoForDuration(ADMakeGratitudeList);
        ADDisplayScore();
        ADEndActivity();
    }
}