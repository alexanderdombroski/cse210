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

    // Methods
    public string ADToString() {
        return $"{_ScriptureReference.ADToString()}\n{_ScripturePassage.ADToString()}";
    }
}