using System;
using System.IO;

class ADScripture {
    // Attributes
    private readonly ADReference _ScriptureReference;
    private readonly ADPassage _ScripturePassage;

    // Constructors
    public ADScripture(string P_Reference) {
        // Specified scripture constructor
        _ScriptureReference = new(P_Reference);
        List<string> ADReferenceList = _ScriptureReference.ADExpandReferences();
        string ADVerses = ADFindVerses(ADReferenceList);
        if (ADVerses.StartsWith("\n") || ADVerses == "") {
            Console.WriteLine("Verse not found. Picking Random Verse");
            Console.Write("Press ENTER to continue: ");
            Console.ReadLine();
            string[] ADRandomVerse = ADPickRandomVerse();
            _ScriptureReference = new(ADRandomVerse[0]);
            _ScripturePassage = new(ADRandomVerse[1]);
        } else {
            _ScripturePassage = new(ADVerses);
        }
    }

    public ADScripture() {
        // Random Verse Constructor
        string[] ADVerse = ADPickRandomVerse();
        _ScriptureReference = new(ADVerse[0]);
        _ScripturePassage = new(ADVerse[1]);
    }

    // Methods
    private static string ADFindVerses(List<string> P_References) {
        // CREATIVITY - Pull one or more verses from a file
        // Create a dictionary of found references and passages
        Dictionary<string, string> ADVersesDict = P_References.ToDictionary(key => key, key => (string)null);
        
        // Iterate thorough file and populate the dictionary
        string[] ADLines = File.ReadAllLines("scripture.txt");
        foreach (string verse in ADLines) {
            string[] ADVerseParts = verse.Split('|');
            if (ADVersesDict.ContainsKey(ADVerseParts[0])) {
                ADVersesDict[ADVerseParts[0]] = ADVerseParts[1];
            }
        }
        
        // Combine the references
        string ADAllPassages = string.Join(" \n", ADVersesDict.Values);
        return ADAllPassages;
    }

    private static string[] ADPickRandomVerse() {
        // Returns a random verse from the file scripture.txt [string reference, string verse]
        string[] ADLines = File.ReadAllLines("scripture.txt"); // CREATIVITY
        Random ADRand = new();
        int ADindex = ADRand.Next(ADLines.Length);
        string line = ADLines[ADindex];
        return line.Split('|');
    }

    public string ADToString() {
        return $"{_ScriptureReference.ADToString()}\n{_ScripturePassage.ADToString()}";
    }

    public bool ADHideWord() {
        return _ScripturePassage.ADHideWord();
    }

    public static void ADRememberScripture(string P_Reference, string P_Scripture) {
        // Appends a verse to the file scripture.txt 
        using (StreamWriter ADFileWriter = File.AppendText("scripture.txt")) {
            ADFileWriter.Write($"\n{P_Reference}|{P_Scripture}");
        }
    }
}