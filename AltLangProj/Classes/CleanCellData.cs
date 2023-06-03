using System.Text.RegularExpressions;

namespace AltLangProj.Classes;

/// <summary>
/// This Class cleans the data that was parsed from the given 'cells' csv file.
///
/// Note: I separated this method from the ParseCsvFile class, so the ParseCsvFile class
/// could be reused for parsing other csv files. In addition to cleaning the data, the class
/// includes a method for verifying that all the missing data was replaced (with null). Also, in an
/// effort to display a cell record so that it all fits nicely in the console (when fullscreen) I
/// further cleaned some of the descriptive (string) data that I wasn't using for any analysis. For
/// one example, the body dimensions data now just displays the dimensions in mm (stripping the alternative
/// measurement in inches).   
/// </summary>
public class CleanCellData
{
    private List<List<string>> _cleanColumnData;

    /// <summary>
    /// This Constructs a CleanCellData object.
    /// </summary>
    /// <param name="columnData"> The parsed data (column oriented) </param>
    /// <param name="headers"> The list of column titles </param>
    public  CleanCellData(List<List<string>> columnData, List<string> headers)
    {
        _cleanColumnData = new List<List<string>>();
        CleanData(columnData, headers);
    }

    /// <summary>
    /// This method cleans the data parsed from the given 'cells' csv file.
    /// Note: because each column has the same data type, I implemented this
    /// method to clean the data by column.
    /// </summary>
    /// <param name="columnData"> The parsed data (column oriented) </param>
    /// <param name="headers"> The list of column titles </param>
    private void CleanData(List<List<string>> columnData, List<string> headers)
    {
        for (int i = 0; i < columnData.Count; i++)
        {
            List<string> temp = new List<string>();
            for (int j = 0; j < columnData[0].Count; j++)
            {
                if (columnData.ElementAt(i).ElementAt(j) == "" || 
                    columnData.ElementAt(i).ElementAt(j) == "-" || 
                    (columnData.ElementAt(i).ElementAt(j) == "V1" && headers[i] != "features_sensors") ||
                    columnData.ElementAt(i).ElementAt(j) == "No" ||
                    columnData.ElementAt(i).ElementAt(j) == "Yes")
                {
                    temp.Add(null);
                }
                else if (headers[i] == "launch_announced" || headers[i] == "launch_status")
                {
                    Regex r = new(@"\b\d{4}\b");
                    Match m = r.Match(columnData[i][j]);
                    if (m.Success)
                    {
                        temp.Add(m.Value.Replace("\"", "")); 
                    }
                    else
                    {
                        if (columnData.ElementAt(i).ElementAt(j) == "Cancelled" ||
                            columnData.ElementAt(i).ElementAt(j) == "Discontinued")
                        {
                            temp.Add(columnData.ElementAt(i).ElementAt(j).Replace("\"", "").Replace(" ",""));
                        }
                        else
                        {
                            temp.Add(null);
                        }
                    }
                }
                else if (headers[i] == "body_weight" || headers[i] == "display_size")
                {
                    Regex r = new (@"([^\s]+)");
                    Match m = r.Match(columnData[i][j]);
                    if (m.Success)
                    {
                        temp.Add(m.Value.Replace("\"", ""));
                    }
                }
                else if (headers[i] == "platform_os")
                {
                    temp.Add(columnData.ElementAt(i).ElementAt(j).Split(",")[0].Replace("\"", "").Replace(
                        " Professional", "").Split(" (")[0].Replace("Microsoft Smartphone", "Microsoft Windows Mobile").Replace(
                        " Smartphone", "").Replace(" PocketPC", "").Replace(" for Phone Edition", "").Replace(
                        " SE", "").Replace(" OS", "").Replace("for", ""));
                }
                else if (headers[i] == "body_dimensions")
                {
                    if (columnData.ElementAt(i).ElementAt(j).Contains("F")) 
                    {
                        temp.Add(columnData.ElementAt(i).ElementAt(j).Split("F")[1].Split(": ")[1].Replace("\"", ""));
                    }
                    else
                    {
                        temp.Add(columnData.ElementAt(i).ElementAt(j).Split("(")[0].Split(",")[0].Replace("\"", ""));
                    }
                    int size = temp.Count() - 1;
                    string tempDimension = temp[size].Replace(" mm", "").Replace("mm", "") + "mm";
                    temp[size] = tempDimension;
                }
                else if (headers[i] == "display_resolution")
                {
                    temp.Add(columnData.ElementAt(i).ElementAt(j).Split(",")[0].Replace("\"", "").Replace("pixels", "px").Split(" (")[0]);
                    int size = temp.Count() - 1;
                    if (temp[size].Contains("lines") || temp[size].Contains("Lines") || temp[size].Contains("chars"))
                    {
                        temp[size] = null;
                    }
                }
                else if (headers[i] == "body_sim")
                {
                    temp.Add(columnData.ElementAt(i).ElementAt(j).Split("(")[0].Replace("\"", "").Replace("Pre-installed ", 
                        "").Replace(" card &", ","));
                }
                else if (headers[i] == "display_type")
                {
                    temp.Add(columnData.ElementAt(i).ElementAt(j).Split(",")[0].Replace("\"", "").Replace(
                        " capacitive", "").Replace(" resistive", "").Replace("Foldable ", "").Replace("Flexible ", ""));
                }
                else if (headers[i] == "model")
                {
                    temp.Add(columnData.ElementAt(i).ElementAt(j).Split(" (")[0].Replace("\"", ""));
                }
                else if (headers[i] == "features_sensors")
                {
                    string fs = columnData.ElementAt(i).ElementAt(j).Replace("\"", "");
                    fs = Regex.Replace(fs, @"\((.*?)\)", "").Replace(" ,", ",");
                    temp.Add(fs);
                }
                else
                {
                    temp.Add(columnData.ElementAt(i).ElementAt(j).Replace("\"", ""));
                }
            }
            _cleanColumnData.Add(temp);
        }
    }

    /// <summary>
    /// This method gets the cleaned data (column oriented)
    /// </summary>
    /// <returns> A matrix of clean column data </returns>
    public List<List<string>> GetCleanColumnData()
    {
        return this._cleanColumnData;
    }

    /// <summary>
    /// This method checks that the cleaned data had all missing data replaced (with null).
    /// </summary>
    /// <returns> True if there is missing data, and False otherwise </returns>
    public Boolean HasMissingData()
    {
        Boolean missingData = false;
        foreach (List<string> columnData in _cleanColumnData)
        {
            foreach (string element in columnData)
            {
                if (element is "" or "-")
                {
                    missingData = true;
                    break;
                }
            }
            if (missingData)
            {
                break;
            }
        }
        return missingData;
    }

    /// <summary>
    /// This method prints whether or not there is any missing data (after the data is cleaned).
    /// </summary>
    public void PrintHasMissingData()
    {
        if (HasMissingData())
        {
            Console.WriteLine("[The data has missing values]");
        }
        else
        {
            Console.WriteLine("[The data doesn't have missing values]");
        }
    }
}