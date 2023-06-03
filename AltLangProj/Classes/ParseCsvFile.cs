using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AltLangProj.Classes;

public class ParseCsvFile
{
    private List<List<string>> rowData;
    private List<List<string>> columnData;

    public ParseCsvFile(string filePath)
    {
        this.rowData = new List<List<string>>();
        this.columnData = new List<List<string>>();
        parse(filePath);
    }

    public void parse(string filePath)
    {
        // avoid file not found exception
        if (File.Exists(filePath))
        {
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

    public List<List<string>> getRowData()
    {
        return this.rowData;
    }

    public List<List<string>> getColumnData()
    {
        return this.columnData;
    }
}