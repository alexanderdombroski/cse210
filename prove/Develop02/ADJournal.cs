// Alex Dombroski - 03/08/2024

using System;

public class ADJournal {
    // Attributes
    public string _ADName;
    public List<ADEntry> _ADEntries = new();

    // Methods
    public ADJournal() {}
    public List<string> ADToString() {
        // Returns a list of console-writeable strings of all class data
        List<string> ADJournalEntries = new() {
            $"----- {_ADName}'s Journal -----"
        };
        _ADEntries.ForEach(entry => {
            ADJournalEntries.Add(entry.ADToString());
        });
        return ADJournalEntries;
    }
    public List<string> ADToCsv() {
        // Returns a list of lines formatted to be stored in a text/csv file
        List<string> lines = new() {$"{_ADName}\n"};
        _ADEntries.ForEach(entry => {
            lines.Add($"{entry.ADToCsv()}\n");
        });
        lines[^1] = lines[^1].Trim(); // Removes the last newline
        return lines;
    }
}