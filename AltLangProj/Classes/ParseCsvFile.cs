using System.Text.RegularExpressions;

namespace AltLangProj.Classes;

/// <summary>
/// This Class parses the given csv file (but would work with other csv files).
///
/// Note: My implementation stores the parsed data in both column and row oriented lists.
/// </summary>
public class ParseCsvFile
{
    private List<List<string>> rowData;
    private List<List<string>> columnData;

    /// <summary>
    /// This Constructs a new ParseCsvFile object.
    /// </summary>
    /// <param name="filePath"> The filepath for a csv file </param>
    public ParseCsvFile(string filePath)
    {
        this.rowData = new List<List<string>>();
        this.columnData = new List<List<string>>();
        parse(filePath);
    }

    /// <summary>
    /// This method parses a csv file, storing the data.
    /// </summary>
    /// <param name="filePath"> The filepath for a csv file </param>
    public void parse(string filePath)
    {
        // avoid file not found exception
        if (File.Exists(filePath))
        {
            // avoid parsing an empty file
            if (new FileInfo(filePath).Length == 0)
            {
                Console.WriteLine("File Is Empty");
            }
            else
            {
                using (var reader = new StreamReader(filePath))
                {
                    List<string> temp = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var values = Regex.Split(line, "[,]{1}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                        this.rowData.Add(values.ToList());
                    }
                }

                for (int i = 0; i < this.rowData[0].Count; i++)
                {
                    List<string> tempList = new List<string>();
                    for (int j = 1; j < this.rowData.Count; j++)
                    {
                        tempList.Add(rowData.ElementAt(j).ElementAt(i));
                    }
                    columnData.Add(tempList);
                }
            }
        }
        else
        {
            Console.WriteLine("File Does Not Exist");
        }
    }

    /// <summary>
    /// This method gets the parsed row data.
    /// </summary>
    /// <returns> A matrix of row data </returns>
    public List<List<string>> getRowData()
    {
        return this.rowData;
    }

    /// <summary>
    /// This method gets the parsed column data.
    /// </summary>
    /// <returns> A matrix of column data </returns>
    public List<List<string>> getColumnData()
    {
        return this.columnData;
    }
}