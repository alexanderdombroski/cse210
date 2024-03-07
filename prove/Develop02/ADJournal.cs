using System;

public class ADJournal {
    // Attributes
    public string _ADName;
    public List<ADEntry> _ADEntries = new();

    // Methods
    public ADJournal() {}
    public List<string> ADToString() {
        List<string> ADJournalEntries = new();
        _ADEntries.ForEach(entry => {
            ADJournalEntries.Add(entry.ADToString());
        });
        return ADJournalEntries;
    }
    public List<string> ADToCsv() {
        List<string> lines = new();
        _ADEntries.ForEach(entry => {
            lines.Add($"{entry.ADToCsv()}\n");
        });
        lines[^1] = lines[^1].Trim();
        return lines;
    }
}