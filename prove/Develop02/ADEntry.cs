// Alex Dombroski - 03/08/2024

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
        // Returns a writable string
        return $"{_ADDate} - {_ADPrompt}\n    {_ADResponse}";
    }
    public string ADToCsv() {
        // returns data formatted as a line in a CSV, without the newline
        return $"{_ADDate}|{_ADPrompt}|{_ADResponse}";
    }
}