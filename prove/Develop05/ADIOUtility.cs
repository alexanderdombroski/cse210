public static class ADIOUtility {
    // Methods:
    public static int ADGetValidInt(string P_Prompt, int P_LowerBound, int P_UpperBound) {
        // Repeatedly asks user for a menu option choice until a valid answer is given
        bool ADInvalidResponse;
        int ADReturnValue;
        do {
            Console.Write(P_Prompt);
            string ADResponse = Console.ReadLine();
            ADInvalidResponse = !int.TryParse(ADResponse, out ADReturnValue);
        } while (ADReturnValue < P_LowerBound || ADReturnValue > P_UpperBound || ADInvalidResponse);
        return ADReturnValue;
    }
    public static object[] ADUnparseArray(object[] P_Array) {
        object[] ADReturnArray = new object[P_Array.Length];
        for(int i=0; i<P_Array.Length; i++) {
            if (int.TryParse(P_Array[i] as string, out int ADIntValue)) {
                ADReturnArray[i] = ADIntValue;
            } else if (bool.TryParse(P_Array[i] as string, out bool ADBoolValue)) {
                ADReturnArray[i] = ADBoolValue;
            } else {
                ADReturnArray[i] = P_Array[i];
            }
        }
        return ADReturnArray;
    }
    public static string ADGetFileName(string P_Prompt) {
        Console.Write(P_Prompt);
        string ADFileName = Console.ReadLine();
        return ADFileName.Contains('.') ? ADFileName : ADFileName + ".txt";
    }
}