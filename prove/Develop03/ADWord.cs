using System;
using System.Linq;

class ADWord {
    // Attributes:
    private string _ADword;
    
    // Constructors:
    public ADWord(string P_Word) {
        _ADword = P_Word;
    }
    
    // Methods:
    public string ADToString() {
        return _ADword;
    }

    public void ADClearWord() {
        _ADword = (_ADword.StartsWith("\n") ? "\n" : "") + new string('_', _ADword.Length);
    }
}