class ADAssignment {
    // Attributes:
    private string _ADStudentName;
    private string _ADTopic;

    // Constructors:
    public ADAssignment(string P_StudentName, string P_Topic) {
        _ADStudentName = P_StudentName;
        _ADTopic = P_Topic;
    }

    // Methods:
    public string ADGetSummary() {
        return $"{_ADStudentName} - {_ADTopic}";
    }
}

class ADMathAssignment : ADAssignment {
    // Attributes:
    private string _ADTextbookSection;
    private string _ADProblems;

    // Constructors:
    public ADMathAssignment(string P_StudentName, string P_Topic, string P_TextbookSection, string P_Problems) : base(P_StudentName, P_Topic) {
        _ADTextbookSection = P_TextbookSection;
        _ADProblems = P_Problems;
    }

    // Methods:
    public string ADGetHomeworkList() {
        return $"{ADGetSummary()}\nSection {_ADTextbookSection} Problems {_ADProblems}";
    }
}

class ADWritingAssignment : ADAssignment {
    // Attributes:
    private string _ADTitle;

    // Constructors:
    public ADWritingAssignment(string P_StudentName, string P_Topic, string P_Title) : base(P_StudentName, P_Topic) {
        _ADTitle = P_Title;
    }

    // Methods:
    public string ADGetWritingInformation() {
        return $"{ADGetSummary()}\n{_ADTitle}";
    }
}

