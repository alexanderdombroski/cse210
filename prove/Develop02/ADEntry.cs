using System;

public class ADEntry {
    // Attributes
    public string _ADDate;
    public string _ADPrompt;
    public string _ADResponse;

    // Methods
    public ADEntry() {
        DateTime ADCurrentTime = DateTime.Now;
        _ADDate = ADCurrentTime.ToShortDateString();
    }

    public string ADToString() { 
        return $"{_ADDate} - {_ADPrompt}\n    {_ADResponse}";
    }
    public string ADToCsv() {
        return $"{_ADDate}|{_ADPrompt}|{_ADResponse}";
    }
}