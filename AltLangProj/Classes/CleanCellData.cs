using System.Security.Principal;
using System.Text.RegularExpressions;

namespace AltLangProj.Classes;

public class CleanCellData
{
    private List<List<string>> cleanColumnData;

    public  CleanCellData(List<List<string>> columnData, List<string> headers)
    {
        cleanColumnData = new List<List<string>>();
        cleanData(columnData, headers);
    }

    private void cleanData(List<List<string>> columnData, List<string> headers)
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
                    //Regex r = new Regex(@"^[^\d]*(\d{4})");
                    Regex r = new Regex(@"\b\d{4}\b");
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
                            temp.Add(columnData.ElementAt(i).ElementAt(j).Replace("\"", ""));
                        }
                        else
                        {
                            temp.Add(null);
                        }
                    }

                }
                else if (headers[i] == "body_weight" || headers[i] == "display_size")
                {
                    Regex r = new Regex(@"([^\s]+)");
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
                else
                {
                    temp.Add(columnData.ElementAt(i).ElementAt(j).Replace("\"", ""));
                }
            }
            cleanColumnData.Add(temp);
        }
    }

    public List<List<string>> getCleanColumnData()
    {
        return this.cleanColumnData;
    }
}