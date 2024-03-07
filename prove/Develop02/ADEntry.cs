using System;

public class ADEntry {
    // Attributes
    public string _ADDate;
    public string _ADPrompt;
    public string _ADResponse;

    // Methods
    public string ADToString() { 
        return $"{_ADDate} - {_ADPrompt}\n\t{_ADResponse}";
    }
    public string ADToCsv() {
        return $"{_ADDate}|{_ADPrompt}|{_ADResponse}";
    }
}