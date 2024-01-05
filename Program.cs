using PrimaryIndexSearch;
using SecondaryIndexSearch;

// Entry point
int method = 2; // 1 for primary index, 2 for secondary index
switch (method)
{
    case 1:
        PrimarySearch.Search();
        break;
    case 2:
        SecondaryIndex.search();
        break;
}
