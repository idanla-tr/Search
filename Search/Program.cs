using System;
using System.Collections.Generic;
using System.IO;

using FuzzySharp;

class Program
{
    static void Main()
    {
        // Replace with the directory you want to start the search from (C drive in this case).
        string searchDirectory = @"C:\Code";

        // Replace with the fuzzy search term.
        string searchTerm = "lab";

        // List of filenames that match the fuzzy search term.
        List<string> matchingFiles = new List<string>();

        // Define a minimum threshold for fuzzy matching.
        double threshold = 0.5; // Adjust as needed.

        SearchFilesRecursively(searchDirectory, searchTerm, matchingFiles, threshold);

        if (matchingFiles.Count > 0)
        {
            Console.WriteLine("Matching files:");
            foreach (string matchingFile in matchingFiles)
            {
                Console.WriteLine(matchingFile);
            }
        }
        else
        {
            Console.WriteLine("No matching files found.");
        }
    }

    static void SearchFilesRecursively(string directory, string searchTerm, List<string> matchingFiles, double threshold)
    {
        try
        {
            // Get all files in the current directory.
            string[] files = Directory.GetFiles(directory);

            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                double similarity = Fuzz.PartialRatio(fileName, searchTerm);

                if (similarity >= threshold)
                {
                    matchingFiles.Add(file);
                }
            }

            // Recursively search subdirectories.
            string[] subDirectories = Directory.GetDirectories(directory);
            foreach (string subDirectory in subDirectories)
            {
                SearchFilesRecursively(subDirectory, searchTerm, matchingFiles, threshold);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Access denied to directory: " + directory);
        }
    }
}