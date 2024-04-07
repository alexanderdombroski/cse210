using System;
using System.Diagnostics;
using System.IO;

public static class CodeReader {
    // Methods:
    public static string[] ReadInCode(string filePath) {
        // Attempts to read in code, and and has optional functionality to handle if the file is locked by a process. 
        // The file should not be locked if it was written with VSCode
        int maxAttempts = 5;
        int attempts = 0;
        while (attempts < maxAttempts) {
            try {
                string[] fileLines = File.ReadAllLines(filePath);
                return fileLines;
            } catch (Exception ex) {
                if (IsFileLocked(filePath)) {
                    attempts++;
                    Console.Write($"Attempt {attempts}/{maxAttempts}: File is locked, retrying in 2 second ");
                    ConsoleUtility.PauseMiliseconds(2000);
                } else {
                    Console.Write($"An error occurred: {ex.Message}");
                    ConsoleUtility.PauseMiliseconds(10000);
                    return null;
                }
            }
        }
        if (IsFileLocked(filePath)) {
            MenuUtility.RunMenu(
                "Would you like to search for and kill processes related to this file?",
                new List<string> {
                    "Yes",
                    "No (quit)"
                },
                new List<Action> {
                    () => KillFileProcesses(filePath),
                }
            );
        }
        try {
            string[] fileLines = File.ReadAllLines(filePath);
            Console.WriteLine("Read successful");
            return fileLines;
        } catch {
            Console.Write("Maximum attempts reached. Could not read the file.");
            ConsoleUtility.PauseMiliseconds(1000);
            return null;
        }
    }
    private static void KillFileProcesses(string filePath) {
        List<Process> fileProcesses = FindProcessLockingFile(filePath);
        if (fileProcesses.Count == 0) {
            Console.WriteLine("Could not any file-related processes to kill");
        } else {
            foreach (Process fileProcess in fileProcesses) {
                if (fileProcess != null) {
                    fileProcess.Kill();
                    Console.WriteLine($"Process {fileProcess.ProcessName} holding lock on file terminated.");
                    try {
                        string[] fileLines = File.ReadAllLines(filePath);
                        Console.WriteLine("Read successful");
                        break;
                    } catch {
                        Console.Write("File still cannot be read ");
                        ConsoleUtility.PauseMiliseconds(1000);
                    }
                }
            }   
        }
    }
    public static bool IsFileLocked(string filePath) { // ChatGPT Helper method to check if the file is locked
        #pragma warning disable IDE0059, CS0168
        try {
            using FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            // File is not locked, so close it and return false
            fileStream.Close();
            return false;
        }
        catch (IOException ex) { 
            return true;
        }
        #pragma warning restore IDE0059, CS0168
    }
    public static List<Process> FindProcessLockingFile(string filePath) {
        Process[] processes = Process.GetProcesses();
        List<Process> FileProcesses = new();
        foreach (Process process in processes) {
            try {
                foreach (ProcessModule module in process.Modules) {
                    if (module.FileName.Equals(filePath, StringComparison.OrdinalIgnoreCase)) {
                        FileProcesses.Add(process);
                        break; // stop checking module
                    }
                }
            } catch (Exception) {
                // Ignore exceptions thrown when accessing process information
            }
        }
        // No process holding the lock found
        return FileProcesses;
    }
}