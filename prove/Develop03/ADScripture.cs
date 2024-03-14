using System;
using System.IO;

class ADScripture {
    // Attributes
    private readonly ADReference _ScriptureReference;
    private readonly ADPassage _ScripturePassage;

    // Constructors
    public ADScripture(string P_Reference, string P_Verse) {
        
    }

    public ADScripture() {
        string[] ADverse = ADReadVerse();
        _ScriptureReference = new(ADverse[0]);
        _ScripturePassage = new(ADverse[1]);
    }
    
    private static string[] ADReadVerse(string referece = null) {
        string[] ADLines = File.ReadAllLines("scripture.txt");
        if (referece == null) {
            Random ADRand = new();
            int ADindex = ADRand.Next(ADLines.Length);
            string line = ADLines[ADindex];
            return line.Split('|');
        } else {
            foreach (string item in ADLines) {
                
            }
            return ADLines;
        }
    }

    // Methods
    public string ADToString() {
        return $"{_ScriptureReference.ADToString()}\n{_ScripturePassage.ADToString()}";
    }
}