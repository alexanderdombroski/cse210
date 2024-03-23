using System.Linq;
class ADReflection : ADActivity, ADActivity.ADIRunnable {
    // Attributes:
    private List<string> _ADQuestionList;

    // Constructors:
    public ADReflection(string P_StartingMessage, string P_EndingMessage, List<string> P_Prompts, List<string> P_Questions) : base(P_StartingMessage, P_EndingMessage, P_Prompts) {
        _ADQuestionList = P_Questions;
    }
    
    // Methods:




    public void ADRun() {
        Console.WriteLine("running!");
    }
}