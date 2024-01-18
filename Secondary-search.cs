using System.Globalization;
using System.IO;
using System.Linq;
using CsvDemo;
using CsvHelper;
using CSVSIndex;
using JobIndexDemo;

namespace SecondaryIndexSearch;

public static class SecondaryIndex
{
    private static string mainSecondaryFile = "datafiles\\SecondarySearchFiles\\Jobs.csv";
    private static string dataFile = "datafiles\\SecondarySearchFiles\\non-ordered-data.csv";

    private static string recordsToFind()
    {
        string? line = Console.ReadLine();

        if (line is null)
            line = "not  Valid Input";

        return line;
    }

    private static bool isValid(ref List<string> jobs, string target)
    {
        if (jobs.Contains(target))
            return true;

        return false;
    }

    private static string getTargetReqFile(ref Secondary[] files, string target)
    {
        int right = files.Length - 1;
        int left = 0;
        while (left <= right)
        {
            int mid = (right + left) / 2;

            if (files[mid].jobTitle.CompareTo(target) == 0)
                return files[mid].jobFile;

            if (files[mid].jobTitle.CompareTo(target) < 0)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return "Error";
    }

    private static void ReadIndexFile(ref Secondary[] files)
    {
        using var reader = new StreamReader(mainSecondaryFile);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Secondary>();
        int i = 0;

        foreach (var record in records)
        {
            files[i] = record;
            i++;
        }
    }

    private static void getTargetIndexes(ref List<JobIndex> jobindex, string targetJobFile)
    {
        using var reader = new StreamReader(targetJobFile);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<JobIndex>();

        foreach (var record in records)
        {
            jobindex.Add(record);
        }
    }

    private static void getTargetRecords(ref List<JobIndex> jobindex)
    {
        using var reader = new StreamReader(dataFile);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Person>();

        foreach (var record in records)
        {
            if (jobindex.Any(job => job.Index == record.Index)) // if save in a list and then print => O(n) + O(n)
            {
                Console.WriteLine($"Index: {record.Index}");
                Console.WriteLine($"User ID: {record.UserId}");
                Console.WriteLine($"First Name: {record.FirstName}");
                Console.WriteLine($"Last Name: {record.LastName}");
                Console.WriteLine($"Sex: {record.Sex}");
                Console.WriteLine($"Email: {record.Email}");
                Console.WriteLine($"Phone: {record.Phone}");
                Console.WriteLine($"Date of Birth: {record.DateOfBirth.ToShortDateString()}");
                Console.WriteLine($"Job Title: {record.JobTitle}\n\n");
            }
        }
    }

    public static void search(string target)
    {
        List<string> jobs = new List<string>
        {
            "Cybersecurity Analyst",
            "Data Analyst",
            "Network Engineer",
            "Software Developer",
            "Systems Administrator",
            "IT Project Manager",
            "Cloud Solutions Architect",
            "Database Administrator",
            "AI/Machine Learning Engineer",
            "IT Support Specialist",
            "CEO"
        };

        if (!isValid(ref jobs, target))
            throw new("Not A valid Job Title");

        Secondary[] files = new Secondary[11];
        List<JobIndex> jobindex = new();
        List<Person> peopleRecords = new();
        ReadIndexFile(ref files);
        string reqFileName = getTargetReqFile(ref files, target);

        string targetJobFile = @"datafiles\SecondarySearchFiles\" + reqFileName + ".csv";
        getTargetIndexes(ref jobindex, targetJobFile);
        getTargetRecords(ref jobindex); // not optimal??????
    }
}
