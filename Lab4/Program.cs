// Lab 4
// Student name: Zeren Ulutas
// Student number: 18622894

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

Console.WriteLine("CPEN223 Lab 4");

//Testing: Write test cases that exercise all four methods you are to implement.
//TODO
//Dictionary<string, int> counts = GenomeAnalyzer.CountKMers("AAAA", 2);
//Console.WriteLine($"Expected: AA -> 3, Actual: AA -> {counts["AA"]} ({counts.Count} entries)");

//Dictionary<string, int> counts = GenomeAnalyzer.CountKMers("AAAA", 0);

// Dictionary<string, int> counts = GenomeAnalyzer.CompareProfiles("ACGACG", "ACGTCG", 3);
// Console.WriteLine($"Expected: ACG -> -1, Actual: ACG -> {counts["ACG"]}");
// Console.WriteLine($"Expected: CGA -> -1, Actual: CGA -> {counts["CGA"]}");
// Console.WriteLine($"Expected: GAC -> -1, Actual: GAC -> {counts["GAC"]}");
// Console.WriteLine($"Expected: CGT -> 1, Actual: CGT -> {counts["CGT"]}");
// Console.WriteLine($"Expected: GTC -> 1, Actual: GTC -> {counts["GTC"]}");
// Console.WriteLine($"Expected: TCG -> 1, Actual: TCG -> {counts["TCG"]}");

//
// bool differ = GenomeAnalyzer.SamplesDiffer("AAAA", "TTTT", 2, 3);
// Console.WriteLine($"Expected: True, Actual: {differ}");

//List<string> counts = GenomeAnalyzer.MostChangedKMers("ACGACG", "ACGTCG", 3);
//Console.WriteLine($"Expected: CGT GTC TCG, Actual: {counts}");

List<string> counts = GenomeAnalyzer.MostChangedKMers("ACGACG", "ACGTCG", 3);

// Use string.Join to format the list elements
Console.WriteLine($"Expected: CGT GTC TCG, Actual: {string.Join(" ", counts)}");

//end Testing code

//Do not change the program skeleton: keep the class name, method names,
//parameters, and return types exactly as given.
//Do not use LINQ, and do not use Console inside the GenomeAnalyzer methods.

public static class GenomeAnalyzer
{
    public static Dictionary<string, int> CountKMers(string sequence, int k)
    {
        Dictionary<string, int> counts = new();

        if (sequence == null || k <= 0 || k > sequence.Length)
        {
            throw new ArgumentException();
        }

        for (int index = 0; index <= sequence.Length - k; index++)
        {
            string kmers = sequence.Substring(index, k);
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
        if (reference == null || sample == null || k <= 0)
        {
            throw new ArgumentException();
        }

        Dictionary<string, int> refMers = CountKMers(reference, k);
        Dictionary<string, int> sampMers = CountKMers(sample, k);
        Dictionary<string, int> change = new();

        foreach (string mers1 in refMers.Keys)
        {
            if (sampMers.ContainsKey(mers1))
            {
                int diff = sampMers[mers1] - refMers[mers1];
                if (diff != 0 )
                {
                    change[mers1] = diff;
                }
            }
            else
            {
                change[mers1] = -refMers[mers1];
            }
        }

        foreach (string mers2 in sampMers.Keys)
        {
            if (!refMers.ContainsKey(mers2))
            {
                change[mers2] = sampMers[mers2];
            }
        }
        return change;
    }

    public static List<string> MostChangedKMers(string reference, string sample, int k)
    {
        if (reference == null || sample == null || k <= 0)
        {
            throw new ArgumentException();
        }

        Dictionary<string, int> allChanges = CompareProfiles(reference, sample, k);
        List<string> mostMers = new();

        int maxVal = 0;

        foreach (string entry in allChanges.Keys)
        {
            if (Math.Abs(allChanges[entry]) == maxVal)
            {
                mostMers.Add(entry);
            }
            else if (Math.Abs(allChanges[entry]) > maxVal)
            {
                maxVal = Math.Abs(allChanges[entry]);
                mostMers.Clear();
                mostMers.Add(entry);
            }
        }
        return mostMers;
    }

    public static bool SamplesDiffer(string reference, string sample, int k, int threshold)
    {
        if (reference == null || sample == null || k <= 0 || threshold < 0)
        {
            throw new ArgumentException();
        }
        
        Dictionary<string, int> allChanges = CompareProfiles(reference, sample, k);
        
        foreach (string entry in allChanges.Keys)
        {
            if (Math.Abs(allChanges[entry]) >= threshold)
            {
                return true;
            }
        }
        return false;
    }
}
