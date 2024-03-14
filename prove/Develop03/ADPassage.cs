using System;
using System.Linq;

class ADPassage {
    // Attributes:
    private readonly List<ADWord> _ADVerseWords = new();
    
    private List<int> _ADHiddenIndexes = new();
    private int _ADWordsLeft = 0;

    // Constructors:
    public ADPassage(string P_Verse) {
        string[] ADVerseWords = P_Verse.Split(' ');
        foreach (string word in ADVerseWords) {
            ADWord ADVerseWord = new(word);
            _ADVerseWords.Add(ADVerseWord);
            _ADWordsLeft++;
        }
        _ADHiddenIndexes.AddRange(Enumerable.Range(0, _ADWordsLeft));
    }

    // Methods:
    public string ADToString(int P_WordsPerLine = 12) {
        string ADReturnString = "";
        for (int i = 0; i < _ADVerseWords.Count; i++) {
            string ADWord = _ADVerseWords[i].ADToString();
            ADReturnString += ADWord + ((i % P_WordsPerLine == P_WordsPerLine-1) ? '\n' : ' ');
        }
        return ADReturnString;
    }

    public bool ADHideWord() {
        if (_ADWordsLeft == 0) {
            return false;
        }
        Random ADRandGen = new();
        int ADWordIndex = ADRandGen.Next(_ADWordsLeft);
        _ADVerseWords[_ADHiddenIndexes[ADWordIndex]].ADClearWord();
        _ADHiddenIndexes.RemoveAt(ADWordIndex);
        _ADWordsLeft--;
        return true;
    }
}