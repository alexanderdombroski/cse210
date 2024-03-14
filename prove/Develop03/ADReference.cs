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
    public ADReference(string P_Book, int P_Chapter, int P_VStart, int P_VEnd) {
        _ADBook = P_Book;
        _ADChapter = P_Chapter;
        _ADVerseStart = P_VStart;
        _ADVerseEnd = P_VEnd;
    }
    public ADReference(ADReference P_Duplicate) {
        _ADBook = P_Duplicate._ADBook;
        _ADChapter = P_Duplicate._ADChapter;
        _ADVerseStart = P_Duplicate._ADVerseStart;
        _ADVerseEnd = P_Duplicate._ADVerseEnd;
    }

    // Methods
    private static (string, int, int, int) ADSplitReference(string P_Reference) {
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
    }

    public string ADGetVerses() {
        if (_ADVerseStart == _ADVerseEnd) {
            return _ADVerseEnd.ToString();
        } else {
            return $"{_ADVerseStart}-{_ADVerseEnd}";
        } 
    }

    public string ADToString() {
        return $"{_ADBook} {_ADChapter}:{ADGetVerses()}";
    }
}

