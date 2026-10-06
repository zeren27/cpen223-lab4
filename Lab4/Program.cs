// Lab 4
// Student name:
// Student number:

using System;
using System.Collections.Generic;

Console.WriteLine("CPEN223 Lab 4");

//Testing: Write test cases that exercise all four methods you are to implement.
//TODO
//Dictionary<string, int> counts = GenomeAnalyzer.CountKMers("AAAA", 2);
//Console.WriteLine($"Expected: AA -> 3, Actual: AA -> {counts["AA"]} ({counts.Count} entries)");

//Dictionary<string, int> counts = GenomeAnalyzer.CountKMers("AAAA", 0);

//
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
        throw new NotImplementedException();
    }

    public static List<string> MostChangedKMers(string reference, string sample, int k)
    {
        throw new NotImplementedException();
    }

    public static bool SamplesDiffer(string reference, string sample, int k, int threshold)
    {
        throw new NotImplementedException();
    }
}
