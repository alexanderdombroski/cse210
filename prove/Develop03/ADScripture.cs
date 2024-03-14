using System;
using System.IO;

class ADScripture {
    // Attributes
    private readonly ADReference _ScriptureReference;
    private readonly ADPassage _ScripturePassage;

    // Constructors
    public ADScripture(string P_Reference) {
        string[] ADVerse = ADFindVerse(P_Reference);
        if (ADVerse.Length == 0) {
            Console.WriteLine("Verse not found. Picking Random Verse");
            Console.Write("Press ENTER to continue: ");
            Console.ReadLine();
            ADVerse = ADPickRandomVerse();
        }
        _ScriptureReference = new(ADVerse[0]);
        _ScripturePassage = new(ADVerse[1]);
    }

    public ADScripture() {
        string[] ADVerse = ADPickRandomVerse();
        _ScriptureReference = new(ADVerse[0]);
        _ScripturePassage = new(ADVerse[1]);
    }

    // Private Methods

    private static string[] ADFindVerse(string P_Reference) {
        string[] ADLines = File.ReadAllLines("scripture.txt");
        string[] ADReturnValue = Array.Empty<string>();
        foreach (string verse in ADLines) {
            string[] ADVerseParts = verse.Split('|');
            if (ADVerseParts[0] == P_Reference) {
                ADReturnValue = ADVerseParts;
            }
        }
        return ADReturnValue;
    }

    private static string[] ADPickRandomVerse() {
        string[] ADLines = File.ReadAllLines("scripture.txt");
        Random ADRand = new();
        int ADindex = ADRand.Next(ADLines.Length);
        string line = ADLines[ADindex];
        return line.Split('|');
    }

    // Public Methods
    public string ADToString() {
        return $"{_ScriptureReference.ADToString()}\n{_ScripturePassage.ADToString()}";
    }

    public bool ADHideWord() {
        return _ScripturePassage.ADHideWord();
    }

    public static void ADRememberScripture(string P_Reference, string P_Scripture) {
        using (StreamWriter ADFileWriter = File.AppendText("scripture.txt")) {
            ADFileWriter.Write($"\n{P_Reference}|{P_Scripture}");
        }
    }
}