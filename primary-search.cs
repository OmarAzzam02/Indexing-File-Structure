using System;
using System.Globalization;
using System.IO;
using CsvDemo;
using CsvHelper;
using CSVPIndex;

namespace PrimaryIndexSearch;

public static class PrimarySearch
{
    private static string primaryIndexFile =
        "C:\\Users\\LENOVO\\Desktop\\PSUT\\DBMS\\DBMS-proj\\app\\datafiles\\Primary Search Files\\primary-Index.csv";
    private static string dataFile =
        "C:\\Users\\LENOVO\\Desktop\\PSUT\\DBMS\\DBMS-proj\\app\\datafiles\\Primary Search Files\\data.csv";

    private static int recordToFind()
    {
        int result = 0; // Default value if conversion fails
        result = Convert.ToInt32(Console.ReadLine());
        return result;
    }

    private static int getBlockNumber(ref Primary[] arr, int target)
    {
        int right = arr.Length - 1;
        int left = 0;
        while (left <= right)
        {
            int mid = (right + left) / 2;

            if (arr[mid].UserId <= target && target <= arr[mid].UserId + 63)
            {
                return arr[mid].Block;
            }

            if (target > arr[mid].UserId)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }

    private static bool isValid(int target)
    {
        if (target < 2024001 || target > 2024500)
        {
            Console.WriteLine("Record Does Not Exist");
            return false;
        }
        return true;
    }

    private static void ReadIndexFile(ref Primary[] Indexes)
    {
        using var reader = new StreamReader(primaryIndexFile);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Primary>();

        int i = 0;
        foreach (var record in records)
        {
            Indexes[i] = record;
            i++;
        }
    }

    private static void ReadDataFile(ref List<Person> peopleRecords, string dataFile, int block)
    {
        using var reader = new StreamReader(dataFile);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Person>();

        // not the optimal solution.
        bool startreading = false;
        foreach (var record in records)
        {
            if (record.Index == block)
                startreading = true;

            if (startreading)
                peopleRecords.Add(record);
        }
    }

    private static void searchRecord(ref List<Person> peopleRecords, int target)
    {
        int right = peopleRecords.Count - 1;
        ;
        int left = 0;

        while (left <= right)
        {
            int mid = (right + left) / 2;

            if (peopleRecords[mid].UserId == target)
            {
                printRecordInfo(peopleRecords[mid]);
                break;
            }

            if (target > peopleRecords[mid].UserId)
                left = mid + 1;
            else
                right = mid - 1;
        }
    }

    private static void printRecordInfo(Person record)
    {
        Console.WriteLine($"Index: {record.Index}");
        Console.WriteLine($"User ID: {record.UserId}");
        Console.WriteLine($"First Name: {record.FirstName}");
        Console.WriteLine($"Last Name: {record.LastName}");
        Console.WriteLine($"Sex: {record.Sex}");
        Console.WriteLine($"Email: {record.Email}");
        Console.WriteLine($"Phone: {record.Phone}");
        Console.WriteLine($"Date of Birth: {record.DateOfBirth.ToShortDateString()}");
        Console.WriteLine($"Job Title: {record.JobTitle}");
    }

    public static void Search()
    {
        int target = 0;
        //
        do
        {
            target = recordToFind();
        } while (!isValid(target));

        // read primary index file
        Primary[] Indexes = new Primary[8];
        ReadIndexFile(ref Indexes);

        // get block number
        int desiredBlock = getBlockNumber(ref Indexes, target);
        Console.WriteLine(desiredBlock);

        // read  the  data file from a specific block + store them in an array
        List<Person> PeopleRecords = new List<Person>();
        ReadDataFile(ref PeopleRecords, dataFile, desiredBlock); // not the optimal solution

        // Search for the record in the array + print the data
        searchRecord(ref PeopleRecords, target);
    }
}
