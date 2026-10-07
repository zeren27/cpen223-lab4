// Lab 4
// Student name: Zeren Ulutas
// Student number: 18622894

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

Console.WriteLine("CPEN223 Lab 4");

//Testing: Write test cases that exercise all four methods you are to implement.
//TODO

// Test case 1 for CountKMers
// Dictionary<string, int> counts = GenomeAnalyzer.CountKMers("AAAA", 2);
// Console.WriteLine($"Expected: AA -> 3, Actual: AA -> {counts["AA"]} ({counts.Count} entries)");

// Test case 2 for CountKMers
// Dictionary<string, int> counts = GenomeAnalyzer.CountKMers("AACGT", 1);
// Console.WriteLine($"Expected: A -> 2, Actual: A -> {counts["A"]} ({counts.Count} entries)");

// Test case 3 for CountKMers
// Dictionary<string, int> counts = GenomeAnalyzer.CountKMers("AAAA", 0);

// Test case 1 for CompareProfiles
// Dictionary<string, int> counts = GenomeAnalyzer.CompareProfiles("ACGACG", "ACGTCG", 3);
// Console.WriteLine($"Expected: CGT -> 1, Actual: CGT -> {counts["CGT"]}");

// Test case 2 for CompareProfiles
// Dictionary<string, int> counts = GenomeAnalyzer.CompareProfiles("AAAA", "TTTT", 2);
// Console.WriteLine($"Expected: AA -> -3, TT -> 3 Actual: AA -> {counts["AA"]}, TT -> {counts["TT"]}");
// AA -> -3, TT -> +3

// Test case 1 for MostChangedKMers
// List<string> counts = GenomeAnalyzer.MostChangedKMers("ACGACG", "ACGTCG", 3);
// Console.WriteLine($"Expected: ACG CGA GAC CGT GTC TCG, Actual: {string.Join(" ", counts)}");

// Test case 2 for MostChangedKMers
// List<string> counts = GenomeAnalyzer.MostChangedKMers("AAAA", "TTTT", 2);
// Console.WriteLine($"Expected: AA TT, Actual: {string.Join(" ", counts)}");

// Test case for SamplesDiffer
// bool differ = GenomeAnalyzer.SamplesDiffer("AAAA", "TTTT", 2, 3);
// Console.WriteLine($"Expected: True, Actual: {differ}");

//end Testing code

//Do not change the program skeleton: keep the class name, method names,
//parameters, and return types exactly as given.
//Do not use LINQ, and do not use Console inside the GenomeAnalyzer methods.

public static class GenomeAnalyzer
{
    public static Dictionary<string, int> CountKMers(string sequence, int k)
    {
        // Create a new dictionary to save the k-mers and their frequencies.
        Dictionary<string, int> counts = new();

        // Check conditions for throwing an Argument Exception.
        if (sequence == null || k <= 0 || k > sequence.Length)
        {
            throw new ArgumentException("Invalid entries.");
        }

        // Go through each base in the sequence to check for invalid entries.
        foreach (char c in sequence)
        {
            if (c != 'A' && c != 'T' && c != 'G' && c != 'C')
            {
                throw new ArgumentException("Invalid base sequence.");
            }
        }

        for (int index = 0; index <= sequence.Length - k; index++)
        {
            // Create a new string to store k-mers.
            string kmers = sequence.Substring(index, k);

            // Check if the dictionary already has the k-mer key. If it does, increment its value by one. If not, assign 1 for
            // its value. This way, you can keep track how many times the k-mer has appeared before.
            if (counts.ContainsKey(kmers))
            {
                counts[kmers]++;
            }
            else
            {
                counts[kmers] = 1;
            }
        }
        return counts;
    }

    public static Dictionary<string, int> CompareProfiles(string reference, string sample, int k)
    {
        // Check conditions for throwing an Argument Exception.
        if (reference == null || sample == null || k <= 0 || k > reference.Length || k > sample.Length)
        {
            throw new ArgumentException("Invalid entries.");
        }

        // Create new dictionaries for reference and sample sequences using the CountKMers to know the frequencies of k-mers beforehand.
        Dictionary<string, int> refMers = CountKMers(reference, k);
        Dictionary<string, int> sampMers = CountKMers(sample, k);

        // Create a new dictionary to compare and store the difference between two k-mers in reference and sample sequences.
        Dictionary<string, int> change = new();

        foreach (string mers1 in refMers.Keys)
        {
            // Check if sample contains the key that reference contains. If so, make a new variable to store the value differences between
            // the sequnces. 
            if (sampMers.TryGetValue(mers1, out int sampCount))
            {
                int diff = sampCount - refMers[mers1];
                // Only store non-zero differences in the dictionary.
                if (diff != 0 )
                {
                    change[mers1] = diff;
                }
                // If the difference between the values is zero, this indicateed that they are identical. In this case, the function should
                // return an empty dictionary.
                else
                {
                    change[mers1] = -refMers[mers1];
                }
            }
            // If the sample sequence doesn't contain that key, assign the negative of its existing value in the reference sequence
            // since "change = count in sample - count in reference".
            else
            {
                change[mers1] = -refMers[mers1];
            }
        }

        // Now go thorugh the sample sequence to only check other k-mers that were unchecked in the reference sequence.
        foreach (string mers2 in sampMers.Keys)
        {
            // If the key is not found in the reference sequence, assign its existing value in the sample sequence since "change = count in
            // sample - count in reference"
            if (!refMers.ContainsKey(mers2))
            {
                change[mers2] = sampMers[mers2];
            }
        }
        return change;
    }

    public static List<string> MostChangedKMers(string reference, string sample, int k)
    {
        // Check conditions for throwing an Argument Exception.
        if (reference == null || sample == null || k <= 0 || k > reference.Length || k > sample.Length)
        {
            throw new ArgumentException("Invalid entries.");
        }

        // Create a new dictionary to store all of the differences in frequencies of keys using the CompareProfiles function.
        Dictionary<string, int> allChanges = CompareProfiles(reference, sample, k);

        // Create a list to store the k-mers with largest difference in frequency.
        List<string> mostMers = new();

        // Create a variable to represent the maximum value in that sequence.
        int maxVal = 0;

        // Go through each k-mer in the sequences.
        foreach (string entry in allChanges.Keys)
        {
            // If the value is bigger than the maximum value, change maximum value to the value corresponding to the current key. 
            // Clear the existing keys in the list whose values are shown to be smaller than the new maximum value. Then add this key
            // to the list. Now, only the key with the maximum value is stored in the list.
            if (Math.Abs(allChanges[entry]) > maxVal)
            {
                maxVal = Math.Abs(allChanges[entry]);
                mostMers.Clear();
                mostMers.Add(entry);
            }
            // If the value is equal to the maximum value, add the key to the list which will be returned in the end.
            else if (Math.Abs(allChanges[entry]) == maxVal)
            {
                mostMers.Add(entry);
            }
        }
        return mostMers;
    }

    public static bool SamplesDiffer(string reference, string sample, int k, int threshold)
    {
        // Check conditions for throwing an Argument Exception.
        if (reference == null || sample == null || k <= 0 || k > reference.Length || k > sample.Length || threshold <= 0)
        {
            throw new ArgumentException("Invalid entries.");
        }
        
        // Create a new dictionary to store all of the differences in frequencies of keys using the CompareProfiles function.
        Dictionary<string, int> changeAll = CompareProfiles(reference, sample, k);
        
        foreach (string entry in changeAll.Keys)
        {
            // Check if the absolute value of the difference in frequencies is equal to or bigger than the threshold for each
            // value in the dictionary. Return true if true.
            if (Math.Abs(changeAll[entry]) >= threshold)
            {
                return true;
            }
        }
        return false;
    }
}
