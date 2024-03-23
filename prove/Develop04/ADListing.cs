using System;

class ADListing : ADActivity, ADActivity.ADIRunnable {
    // Constructors:
    public ADListing(string P_ActivityName, string P_Description, string P_EndingMessage, List<string> P_Prompts) : base(P_ActivityName, P_Description, P_EndingMessage, P_Prompts) {}

    // Methods:
    public void ADRun() {
        ADStartActivity();
        
        


        ADEndActivity();
    }
}
