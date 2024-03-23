using System;
class ADReflection : ADActivity, ADActivity.ADIRunnable {
    // Attributes:
    private List<string> _ADQuestionList;

    // Constructors:
    public ADReflection(string P_ActivityName, string P_Description, string P_EndingMessage, List<string> P_Prompts, List<string> P_Questions) : base(P_ActivityName, P_Description, P_EndingMessage, P_Prompts) {
        _ADQuestionList = P_Questions;
    }
    
    // Methods:




    public void ADRun() {
        Console.WriteLine("running!");
    }
}