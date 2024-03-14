using System;

class ADPassage {
    // Attributes:
    private readonly List<ADWord> _ADVerseWords = new();

    // Constructors:
    public ADPassage(string P_Verse) {
        string[] ADVerseWords = P_Verse.Split(' ');
        foreach (string word in ADVerseWords) {
            ADWord ADVerseWord = new(word);
            _ADVerseWords.Add(ADVerseWord);
        }
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
}