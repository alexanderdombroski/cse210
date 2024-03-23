// CREATIVITY - Added a graditude activity

using System;

class ADGratitude : ADActivity, ADActivity.ADIRunnable {
    // Attributes:
    int _ADItemsListed = 0;

    // Constructors:
    public ADGratitude(string P_ActivityName, string P_Description, string P_EndingMessage, List<string> P_Prompts) : base(P_ActivityName, P_Description, P_EndingMessage, P_Prompts) {}

    // Methods:
    private void ADMakeGratitudeList() {
        Console.WriteLine(ADGetRandomPrompt());
        ADDoForDuration(ADGetGratitudeItem, 15);
    }

    private void ADDisplayInstruction() {
        Console.Write("List answers you are grateful for in the following categories: ");
        ADPauseMiliseconds(4000, new() {"4", "3", "2", "1"}, 1000);
    }

    private void ADGetGratitudeItem() {
        Console.Write("> ");
        Console.ReadLine();
        _ADItemsListed += 1;
    }

    private void ADDisplayScore() {
        Console.Write($"You listed {_ADItemsListed} items! Well done !! ");
        ADPauseMiliseconds(6000);
    }

    public void ADRun() {
        ADStartActivity();
        ADDisplayInstruction();
        ADDoForDuration(ADMakeGratitudeList);
        ADDisplayScore();
        ADEndActivity();
    }
}