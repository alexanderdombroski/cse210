class ADBreathing : ADActivity, ADActivity.ADIRunnable {
    // Constructors:
    public ADBreathing(string P_StartingMessage, string P_EndingMessage, List<string> P_Prompts) : base(P_StartingMessage, P_EndingMessage, P_Prompts) {}

    // Methods:




    public void ADRun() {
        Console.WriteLine("running!");
    }
}