using System;

class ADReference {
    // Attributes
    private readonly string _ADBook;
    private readonly int _ADChapter;
    private readonly int _ADVerseStart;
    private readonly int _ADVerseEnd;
    
    // Constructors
    public ADReference(string P_Reference) {
        var ADParts = ADSplitReference(P_Reference);
        _ADBook = ADParts.Item1;
        _ADChapter = ADParts.Item2;
        _ADVerseStart = ADParts.Item3;
        _ADVerseEnd = ADParts.Item4;
    }

    // Methods
    public List<string> ADExpandReferences() {
        // Creates a reference list of each verse (ie Moroni 10:3, Moroni 10:4, Moroni 10:5)
        List<string> ReferenceList = new();
        for (int i=_ADVerseStart; i<=_ADVerseEnd; i++) {
            ReferenceList.Add($"{_ADBook} {_ADChapter}:{i}");
        }
        return ReferenceList;
    }

    private static (string, int, int, int) ADSplitReference(string P_Reference) {
        // Returns a formatted reference in four parts, matching the class attributes.
        try {
            string[] ADParts = P_Reference.Split(':');
            /* There is no scripture book two letters or shorter, similarly 
            there is no series of books with 10+ book numbers */
            int ADSpaceIndex = ADParts[0].IndexOf(' ', 3); 
            string ADBook = ADParts[0][..ADSpaceIndex];
            int ADChapter = int.Parse(ADParts[0][(ADSpaceIndex + 1)..]);

            int ADVStart;
            int ADVEnd;
            if (ADParts[1].Contains('-')) {
                string[] ADVerseParts = ADParts[1].Split('-');
                ADVStart = int.Parse(ADVerseParts[0]);
                ADVEnd = int.Parse(ADVerseParts[1]);
            } else {
                ADVStart = int.Parse(ADParts[1]);
                ADVEnd = ADVStart;
            }
            return (ADBook, ADChapter, ADVStart, ADVEnd);
        } catch {
            return ("6 Nephi", 24, 2, 4);
        }
    }

    public string ADGetVerses() {
        // Returns the verse section of the reference, (ie "3-5", "6", or "2-6")
        if (_ADVerseStart == _ADVerseEnd) {
            return _ADVerseEnd.ToString();
        } else {
            return $"{_ADVerseStart}-{_ADVerseEnd}";
        } 
    }

    public string ADToString() {
        // Returns the formatted reference
        return $"{_ADBook} {_ADChapter}:{ADGetVerses()}";
    }
}