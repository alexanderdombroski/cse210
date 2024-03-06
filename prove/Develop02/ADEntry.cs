using System;

class ADEntry {
    // Attributes
    string _ADDate;
    string _ADPrompt;
    string _ADResponse;

    // Methods
    string ADToString() { 
        return $"{_ADDate} - {_ADPrompt}\n\t{_ADResponse}";
    }
    string ADToCsv() {
        return $"{_ADDate}|{_ADPrompt}|{_ADResponse}";
    }
}