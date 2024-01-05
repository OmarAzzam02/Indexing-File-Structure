using CsvHelper.Configuration.Attributes;

namespace CSVSIndex;

public class Secondary
{
    [Name("job title")]
    public string jobTitle { get; set; } = "";

    [Name("job file")]
    public string jobFile { get; set; } = "";
}
