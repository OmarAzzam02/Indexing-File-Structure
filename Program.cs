using PrimaryIndexSearch;
using SecondaryIndexSearch;

// Entry point
int method = 2; // 1 for primary index, 2 for secondary index
int UserID = 2024225; // PK  for the primary index Search method
string jobTitle = "Systems Administrator"; // Non PK(Job Title) for the secondary index Search method

switch (method)
{
    case 1:
        try
        {
            PrimarySearch.Search(UserID);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        break;
    case 2:
        try
        {
            SecondaryIndex.search(jobTitle);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        break;
}
