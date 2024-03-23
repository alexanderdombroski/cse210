using System;

class ADBreathing : ADActivity, ADActivity.ADIRunnable {
    // Constructors:
    public ADBreathing(string P_ActivityName, string P_Description, string P_EndingMessage, List<string> P_Prompts) : base(P_ActivityName, P_Description, P_EndingMessage, P_Prompts) {}

    // Methods:
    private static void ADDisplayBreathing() {
        List<string> DotAnimationStages = new() {" ", ".", "..", "..", "", "", ""};
        Console.Write("Breath in, breath out ");
        ADPauseMiliseconds(7000, DotAnimationStages, 1000);
    }

    public void ADRun() {
        ADStartActivity();
        ADDoForDuration(ADDisplayBreathing);
        ADEndActivity();
    }
}